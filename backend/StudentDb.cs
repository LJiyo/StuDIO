using Microsoft.EntityFrameworkCore;

namespace UniversityStudent.Models;

public record aStudent // Represents a student entity
{
    public int Id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? university { get; set; }
    public string? email { get; set; }
    public int age { get; set; }
    public string? degree { get; set; }
}

class StudentDB : DbContext
{
    public StudentDB(DbContextOptions<StudentDB> options) : base(options) { }
    public DbSet<aStudent> Students { get; set; } = null!; // Student records in the database
}
public class Students // Simulates a database of student records
{
    private static List<aStudent> _students = new List<aStudent>
    {
        new aStudent { Id = 1, firstName = "John", lastName = "Doe", university = "University A",
        email = "jDoe@gmail.com", age = 20, degree = "Computer Science" },
        new aStudent { Id = 2, firstName = "Jane", lastName = "Smith", university = "University B",
        email = "smithJane@hotmail.com", age = 22, degree = "Mathematics" },
        new aStudent { Id = 3, firstName = "Alice", lastName = "Johnson", university = "University C",
        email = "allyJohnson@yahoo.com", age = 21, degree = "Business" }
    };

    public static List<aStudent> GetStudents()
    {
        return _students;
    }
    public static aStudent? GetStudentById(int id)
    {
        return _students.SingleOrDefault(s => s.Id == id);
    }
    public static void AddStudent(aStudent student)
    {
        //student.Id = _students.Max(s => s.Id) + 1; increments id?
        _students.Add(student);
    }
    public static aStudent UpdateStudent(aStudent update)
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