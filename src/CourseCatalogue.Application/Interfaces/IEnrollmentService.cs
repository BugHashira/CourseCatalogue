using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Domain;

namespace CourseCatalogue.Application.Interfaces;

public interface IEnrollmentService
{
    Task<ResponseModel<bool>> EnrollAsync(Guid studentId, Guid courseId);
    Task<ResponseModel<IEnumerable<CourseDto>>> GetEnrolledCoursesAsync(Guid studentId);
}
