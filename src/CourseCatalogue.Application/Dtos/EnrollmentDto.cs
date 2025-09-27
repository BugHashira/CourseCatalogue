using System.ComponentModel.DataAnnotations;

namespace CourseCatalogue.Application.Dtos
{
    public class EnrollmentDto
    {
        [Required]
        public Guid StudentId { get; set; }

        [Required]
        public Guid CourseId { get; set; }
    }
}
