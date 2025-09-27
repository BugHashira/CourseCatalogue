using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Application.Interfaces;
using CourseCatalogue.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalogue.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseModel<StudentDto>), 200)]
    [ProducesResponseType(typeof(ResponseModel<StudentDto>), 400)]
    public async Task<IActionResult> Register([FromBody] RegisterStudentDto dto)
    {
        var result = await studentService.RegisterAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseModel<string>), 200)]
    [ProducesResponseType(typeof(ResponseModel<string>), 400)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await studentService.LoginAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseModel<StudentDto>), 200)]
    [ProducesResponseType(typeof(ResponseModel<StudentDto>), 400)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await studentService.GetByIdAsync(id);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 200)]
    [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 400)]
    public async Task<IActionResult> GetAll()
    {
        var result = await studentService.GetAllAsync();
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpPut("update-student")]
    [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
    [ProducesResponseType(typeof(ResponseModel<bool>), 400)]
    public async Task<IActionResult> Update(Guid id, [FromBody] StudentDto dto)
    {
        var result = await studentService.UpdateAsync(id, dto);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize]
    [HttpDelete("delete-student")]
    [ProducesResponseType(typeof(ResponseModel<bool>), 200)]
    [ProducesResponseType(typeof(ResponseModel<bool>), 400)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await studentService.DeleteAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}


