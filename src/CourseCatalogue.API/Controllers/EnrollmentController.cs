using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Application.Interfaces;
using CourseCatalogue.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalogue.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EnrollmentController(IEnrollmentService enrollmentService) : ControllerBase
{
    [HttpPost("enroll")]
    [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
    [ProducesResponseType(typeof(ResponseModel<bool>), 400)]
    public async Task<IActionResult> Enroll([FromBody] EnrollmentDto dto)
    {
        var result = await enrollmentService.EnrollAsync(dto.StudentId, dto.CourseId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("student/{studentId}/courses")]
    [ProducesResponseType(typeof(ResponseModel<IEnumerable<CourseDto>>), 200)]
    [ProducesResponseType(typeof(ResponseModel<IEnumerable<CourseDto>>), 400)]
    public async Task<IActionResult> GetEnrolledCourses(Guid studentId)
    {
        var result = await enrollmentService.GetEnrolledCoursesAsync(studentId);
        return StatusCode(result.StatusCode, result);
    }
}