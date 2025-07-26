using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityStudent.Models;

var builder = WebApplication.CreateBuilder(args); // builder object to configure services
builder.Services.AddEndpointsApiExplorer(); // Adds support for API exploration
builder.Services.AddDbContext<StudentDB>(options => options.UseInMemoryDatabase("items")); // To set up in-memory database for testing
// SQL Server database connection
//builder.Services.AddDbContext<StudentDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Configures DbContext with SQL Server
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Student API",
        Description = "API for managing student records",
        Version = "v1" });
}); // Adds Swagger for API documentation

var app = builder.Build();

// Configures Swagger UI for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger in development mode
    // Swagger JSON file location
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student API v1"));
} 

app.MapGet("/", () => "Hello World!"); // Root endpoint for testing 

// ## CRUD operations for Student entity ##
// Get all students
app.MapGet("/students", async (StudentDB db) => await db.Students.ToListAsync()); // Get all students from the database

// Add a student
app.MapPost("/students", async (StudentDB db, aStudent student) =>
{
    await db.Students.AddAsync(student);
    await db.SaveChangesAsync();
    return Results.Created($"/students/{student.Id}", student); // Adds a new student to the database
});

// Get a student by ID
app.MapGet("/students/{id}", async (StudentDB db, int id) => await db.Students.FindAsync(id));

// Update a student
app.MapPut("/students/{id}", async (StudentDB db, aStudent updateStudent, int id) =>
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
});

app.MapDelete("/students/{id}", async (StudentDB db, int id) =>
{
    var student = await db.Students.FindAsync(id);
    if (student is null) return Results.NotFound();
    db.Students.Remove(student);
    await db.SaveChangesAsync();
    return Results.Ok();
});

//app.MapGet("/students", () => Students.GetStudents());
//app.MapGet("/students/{id}", (int id) => Students.GetStudentById(id));
//app.MapPost("/students", (Student student) => Students.AddStudent(student));
//app.MapPut("/students", (Student student) => Students.UpdateStudent(student));
//app.MapDelete("/students/{id}", (int id) => Students.DeleteStudent(id));


app.Run();
