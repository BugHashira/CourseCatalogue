using System.ComponentModel.DataAnnotations;

namespace CourseCatalogue.Application.Dtos;

public class UpdateCourseDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string Description { get; set; }

    [Required]
    public string Duration { set; get; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Instructor { get; set; }
}
