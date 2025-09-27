namespace CourseCatalogue.Domain.Entities;

public class Enrollment
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
}