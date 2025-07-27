using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityStudent.Models;

var builder = WebApplication.CreateBuilder(args); // builder object to configure services
// builder.Services.AddOpenApi(); // Removed: No such method, Swagger is configured below
// Load environment variables from .env file
//DotNetEnv.Env.Load(".env");

// Access the environment variable for database URI
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("DEFAULT_CONNECTION");
}
else
{
    connection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
}

// Look for "Students" connection string, else, use the StudentDB
//var connectionString = builder.Configuration.GetConnectionString(dbString) ?? "Data Source=StudentDB";

//builder.Services.AddDbContext<StudentDB>(options => options.UseInMemoryDatabase("items")); // To set up in-memory database for testing

// SQL Server database connection
builder.Services.AddDbContext<StudentDB>(options =>
    options.UseSqlServer(connection));

builder.Services.AddEndpointsApiExplorer(); // Adds support for API exploration
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Student API",
        Description = "API for managing student records",
        Version = "v1" });
}); // Adds Swagger for API documentation

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // frontend server
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowFrontend");

// Configures Swagger UI for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger in development mode
    // Swagger JSON file location
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API v1");
    });
} 

app.MapGet("/", () => "Access the Minimal API by adding /swagger to the end of the port link above"); // Root endpoint for testing 

// =================================================================

// ## CRUD operations for Student entity ##
// Get all students
app.MapGet("/studentsList", async (StudentDB db) =>
{
    await db.Students
    .Include(s => s.Groups) // Include related groups if it exists
    .ToListAsync();
})
.WithSummary("Get all the students")
.WithDescription("Retrieve a list of students currently in the database alongside the groups they may be a part of"); // Get all students from the database

// Add a student
app.MapPost("/sign-up", async (StudentDB db, studentUser student) =>
{
    await db.Students.AddAsync(student);
    await db.SaveChangesAsync();
    return Results.Created($"/students/{student.Id}", student); // Adds a new student to the database
})
.WithSummary("Student Sign-up")
.WithDescription("Add a student to the database by signing up and entering relevant details, group sign-up is optional at this stage."); // Get all students from the database


// Get a student by ID
app.MapGet("/studentsList/{id}", async (StudentDB db, int id) => await db.Students.FindAsync(id))
.WithSummary("Get a student by ID");

// Update a student
app.MapPut("/studentsList/{id}", async (StudentDB db, studentUser updateStudent, int id) =>
{
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound(); // Return if student record does not exist
    // Once the student's record is found, update:
    student.firstName = updateStudent.firstName;
    student.lastName = updateStudent.lastName;
    student.university = updateStudent.university;
    student.email = updateStudent.email;
    student.age = updateStudent.age;
    student.degree = updateStudent.degree;
    await db.SaveChangesAsync();
    return Results.NoContent();
})
.WithSummary("Update Student Details");

// Remove a student from list
app.MapDelete("/studentsList/{id}", async (StudentDB db, int id) =>
{
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();
    db.Students.Remove(student);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithSummary("Remove a Student's Details");

// =================================================================
// CRUD operations for login authentication
// Login Authentication (simulated by storing the credentials in DB and comparing upon request)
app.MapPost("login", async (StudentDB db, LoginRequest login) =>
{
    var student = await db.Students.FirstOrDefaultAsync(s => s.username == login.Username && s.password == login.Password);
    
return student is not null ? Results.Ok(student) : Results.Unauthorized();
})
.WithSummary("Student Login Authentication")
.WithDescription("Compares with database if credentials match"); // Get all students from the database;

app.MapGet("/check-username", async (StudentDB db, string username) =>
{
    var exists = await db.Students.AnyAsync(s => s.username == username);
    return Results.Ok(!exists); // true if username is available
})
.WithSummary("Student Username Check")
.WithDescription("Compares with database if username exists already"); // Get all students from the database;

// =================================================================
// CRUD operations for Student Groups
// Create a new group
app.MapPost("/groups", async (StudentDB db, Group group) =>
{
    db.Groups.Add(group);
    await db.SaveChangesAsync();
    return Results.Created($"/groups/{group.Id}", group);
})
.WithSummary("Create a new Group for students to join");


// Delete the whole group
app.MapDelete("/groups/{id}", async (StudentDB db, int id) =>
{
    var group = await db.Groups.FindAsync(id);
    if (group is null) return Results.NotFound();
    db.Groups.Remove(group);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithSummary("Delete an entire existing group");

// Get all existing groups irrespective of students
app.MapGet("/groups", async (StudentDB db) =>
{
    var groups = await db.Groups.ToListAsync();
    return Results.Ok(groups);
})
.WithSummary("Get a list of all existing groups");

// Add student to group
app.MapPost("/groups/{groupId}/add-student/{studentId}", async (StudentDB db, int groupId, int studentId) =>
{
    var group = await db.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
    var student = await db.Students.FindAsync(studentId);

    if (group == null || student == null) return Results.NotFound(); // If group or student does not exist

    // else, valid entry
    group.Members.Add(student);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithSummary("Add a student to a group");

// Remove student from group
app.MapDelete("/groups/{groupId}/remove-student/{studentId}", async (StudentDB db, int groupId, int studentId) =>
{
    var group = await db.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
    var student = await db.Students.FindAsync(studentId);

    if (group == null || student == null) return Results.NotFound();

    group.Members.Remove(student);
    await db.SaveChangesAsync();
    return Results.Ok();
})
.WithSummary("Remove a student to a group");

// Get all groups a student is in
app.MapGet("/studentsList/{id}/groups", async (StudentDB db, int id) =>
{
    var student = await db.Students.Include(s => s.Groups).FirstOrDefaultAsync(s => s.Id == id);
    return student is not null ? Results.Ok(student.Groups) : Results.NotFound();
})
.WithSummary("Find what group(s) a student is in");


app.Run();
