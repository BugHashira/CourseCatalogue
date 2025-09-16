namespace CourseCatalogue.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Duration { get; set; }
    public string Instructor { get; set; }
}