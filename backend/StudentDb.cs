namespace Student.DB;

public record Student // Represents a student entity
{
    public int Id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? university { get; set; }
    public string? email { get; set; }
    public int age { get; set; }
    public string? degree { get; set; }
}

public class StudentDb // Simulates a database for storing student records
{
    private static List<Student> _students = new List<Student>
    {
        new Student { Id = 1, firstName = "John", lastName = "Doe", university = "University A",
        email = "jDoe@gmail.com", age = 20, degree = "Computer Science" },
        new Student { Id = 2, firstName = "Jane", lastName = "Smith", university = "University B",
        email = "smithJane@hotmail.com", age = 22, degree = "Mathematics" },
        new Student { Id = 3, firstName = "Alice", lastName = "Johnson", university = "University C",
        email = "allyJohnson@yahoo.com", age = 21, degree = "Business" }
    };

    public static List<Student> GetStudents()
    {
        return _students;
    }
    public static Student? GetStudentById(int id)
    {
        return _students.SingleOrDefault(s => s.Id == id);
    }
    public static void AddStudent(Student student)
    {
        //student.Id = _students.Max(s => s.Id) + 1; increments id?
        _students.Add(student);
    }
    public static Student UpdateStudent(Student update)
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