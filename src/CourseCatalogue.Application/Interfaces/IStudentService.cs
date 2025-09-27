using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Domain;

namespace CourseCatalogue.Application.Interfaces;

public interface IStudentService
{
    Task<ResponseModel<StudentDto>> RegisterAsync(RegisterStudentDto dto);
    Task<ResponseModel<string>> LoginAsync(LoginDto dto);
    Task<ResponseModel<StudentDto>> GetByIdAsync(Guid id);
    Task<ResponseModel<IEnumerable<StudentDto>>> GetAllAsync();
    Task<ResponseModel<bool>> UpdateAsync(Guid id, StudentDto dto);
    Task<ResponseModel<bool>> DeleteAsync(Guid id);
}
