namespace CourseCatalogue.Domain.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PasswordHash { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new();
}