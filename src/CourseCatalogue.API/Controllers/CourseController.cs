using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Application.Services;
using CourseCatalogue.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalogue.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController(CourseService courseService) : ControllerBase
{
    [HttpPost("add-course")]
    [ProducesResponseType(typeof(ResponseModel<Guid>), 200)]
    [ProducesResponseType(typeof(ResponseModel<Guid>), 400)]
    public async Task<IActionResult> AddCourseAsync([FromBody] CreateCourseDto request)
    {
        var result = await courseService.CreateAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPatch("update-course")]
    [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
    [ProducesResponseType(typeof(ResponseModel<bool>), 400)]
    public async Task<IActionResult> UpdateCourseAsync([FromBody] UpdateCourseDto request)
    {
        var result = await courseService.UpdateAsync(request);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete-course")]
    [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
    [ProducesResponseType(typeof(ResponseModel<bool>), 400)]
    public async Task<IActionResult> DeleteCourseAsync([FromQuery] Guid id)
    {
        var result = await courseService.DeleteAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<CourseDto>), 200)]
    [ProducesResponseType(typeof(ResponseModel<CourseDto>), 400)]
    public async Task<IActionResult> GetCourseAsync([FromRoute] Guid id)
    {
        var result = await courseService.GetByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("courses")]
    [ProducesResponseType(typeof(ResponseModel<IEnumerable<CourseDto>>), 200)]
    [ProducesResponseType(typeof(ResponseModel<IEnumerable<CourseDto>>), 400)]
    public async Task<IActionResult> GetCoursesAsync()
    {
        var result = await courseService.GetAllAsync();
        return StatusCode(result.StatusCode, result);
    }
}