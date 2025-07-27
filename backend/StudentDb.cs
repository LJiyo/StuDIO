using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace UniversityStudent.Models;

public class studentUser // Represents a student entity
{
    public int Id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? university { get; set; }
    public string? email { get; set; }
    public int age { get; set; }
    public string? degree { get; set; }

    // Login credentials for sign-in and sign-up
    public string? username { get; set; }
    public string? password { get; set; }

    // Optional profile editing fields
    public string? skills { get; set; } // Practical skillsets
    public string? projects { get; set; } // Course or Personal projects
    public string? links { get; set; } // e.g. GitHub or LinkedIn
    public string? bio { get; set; } // e.g. "Eager to work on..."

    public List<Group> Groups { get; set; } = new(); // Groups the student is part of
}

class StudentDB : DbContext
{
    public StudentDB(DbContextOptions<StudentDB> options) : base(options) { }
    public DbSet<studentUser> Students { get; set; } = null!; // Student records in the database
    public DbSet<Group> Groups { get; set; }

}
public class testStudents // Simulates a database of student records for In-Memory testing
{
    private static List<studentUser> _students = new List<studentUser>
    {
        new studentUser { Id = 1, firstName = "John", lastName = "Doe", university = "University A",
        email = "jDoe@gmail.com", age = 20, degree = "Computer Science" },
        new studentUser { Id = 2, firstName = "Jane", lastName = "Smith", university = "University B",
        email = "smithJane@hotmail.com", age = 22, degree = "Mathematics" },
        new studentUser { Id = 3, firstName = "Alice", lastName = "Johnson", university = "University C",
        email = "allyJohnson@yahoo.com", age = 21, degree = "Business" }
    };

    // These are for In-Memory CRUD operations
    public static List<studentUser> GetStudents()
    {
        return _students;
    }
    public static studentUser? GetStudentById(int id)
    {
        return _students.SingleOrDefault(s => s.Id == id);
    }
    public static void AddStudent(studentUser student)
    {
        _students.Add(student);
    }
    public static studentUser UpdateStudent(studentUser update)
    {
        _students = _students.Select(student =>
        {
            if (student.Id == update.Id)
            {
                student.firstName = update.firstName;
                student.lastName = update.lastName;
                student.university = update.university;
                student.email = update.email;
                student.age = update.age;
                student.degree = update.degree;
                return student; // Return the updated student
            }
            else
            {
                return student; // Return the unchanged student
            }
        }).ToList();
        return update;
    }
    public static void DeleteStudent(int id)
    {
        _students = _students.FindAll(s => s.Id != id).ToList();
    }
}

// Login 
public class LoginRequest 
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

// Groups the student may be in
public class Group
{
    public int Id { get; set; }
    public string? groupName { get; set; }
    public string? description { get; set; }
    [JsonIgnore]
    public List<studentUser> Members { get; set; } = new();

    protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<studentUser>()
            .HasMany(s => s.Groups)
            .WithMany(g => g.Members);
    }
}


