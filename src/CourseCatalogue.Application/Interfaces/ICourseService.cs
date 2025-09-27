using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Domain;

namespace CourseCatalogue.Application.Interfaces;

public interface ICourseService
{
    Task<ResponseModel<Guid>> CreateAsync(CreateCourseDto createCourse);
    Task<ResponseModel<bool>> DeleteAsync(Guid id);
    Task<ResponseModel<CourseDto>> GetByIdAsync(Guid id);
    Task<ResponseModel<IEnumerable<CourseDto>>> GetAllAsync(string? title, string? instructor, int page, int pageSize);
    Task<ResponseModel<bool>> UpdateAsync(UpdateCourseDto updateCourse);
}
