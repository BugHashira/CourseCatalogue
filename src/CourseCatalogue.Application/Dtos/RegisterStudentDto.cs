using System.ComponentModel.DataAnnotations;

namespace CourseCatalogue.Application.Dtos;

public class RegisterStudentDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
