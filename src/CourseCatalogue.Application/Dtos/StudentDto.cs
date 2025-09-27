using System.ComponentModel.DataAnnotations;

namespace CourseCatalogue.Application.Dtos;

public class StudentDto
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }
}
