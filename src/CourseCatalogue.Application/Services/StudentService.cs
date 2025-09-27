using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Application.Interfaces;
using CourseCatalogue.Domain;
using CourseCatalogue.Domain.Entities;
using CourseCatalogue.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseCatalogue.Application.Services;

public class StudentService(CourseCatalogueContext context, IConfiguration configuration) : IStudentService
{
    public async Task<ResponseModel<StudentDto>> RegisterAsync(RegisterStudentDto dto)
    {
        if (await context.Students.AnyAsync(s => s.Email == dto.Email))
            return ResponseModel<StudentDto>.Failure("Email already exists.");

        var student = new Student
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        await context.Students.AddAsync(student);
        if (await context.SaveChangesAsync() > 0)
        {
            return ResponseModel<StudentDto>.Success(new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth
            }, "Student registered successfully");
        }

        return ResponseModel<StudentDto>.Failure("Failed to register student");
    }

    public async Task<ResponseModel<string>> LoginAsync(LoginDto dto)
    {
        var student = await context.Students.FirstOrDefaultAsync(s => s.Email == dto.Email);
        if (student == null || !BCrypt.Net.BCrypt.Verify(dto.Password, student.PasswordHash))
            return ResponseModel<string>.Failure("Invalid credentials.");

        var token = GenerateJwtToken(student);
        return ResponseModel<string>.Success(token, "Login successful");
    }

    private string GenerateJwtToken(Student student)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, student.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, student.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ResponseModel<StudentDto>> GetByIdAsync(Guid id)
    {
        var student = await context.Students.FindAsync(id);
        if (student == null)
            return ResponseModel<StudentDto>.Success(new StudentDto(), "Student not found");

        var studentDto = new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email,
            DateOfBirth = student.DateOfBirth
        };
        return ResponseModel<StudentDto>.Success(studentDto, "Student retrieved successfully");
    }

    public async Task<ResponseModel<IEnumerable<StudentDto>>> GetAllAsync()
    {
        var students = await context.Students
            .Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                DateOfBirth = s.DateOfBirth
            })
            .ToListAsync();

        return students.Any()
            ? ResponseModel<IEnumerable<StudentDto>>.Success(students, "Students retrieved successfully")
            : ResponseModel<IEnumerable<StudentDto>>.Failure("No students found");
    }

    public async Task<ResponseModel<bool>> UpdateAsync(Guid id, StudentDto dto)
    {
        if (dto == null)
            return ResponseModel<bool>.Failure("Invalid request");

        var student = await context.Students.FindAsync(id);
        if (student == null)
            return ResponseModel<bool>.Success(false, "Student not found");

        student.Name = dto.Name;
        student.Email = dto.Email;
        student.DateOfBirth = dto.DateOfBirth;

        context.Students.Update(student);
        if (await context.SaveChangesAsync() > 0)
            return ResponseModel<bool>.Success(true, "Student updated successfully");

        return ResponseModel<bool>.Failure("Failed to update student");
    }

    public async Task<ResponseModel<bool>> DeleteAsync(Guid id)
    {
        var student = await context.Students.FindAsync(id);
        if (student == null)
            return ResponseModel<bool>.Success(false, "Student not found");

        context.Students.Remove(student);
        if (await context.SaveChangesAsync() > 0)
            return ResponseModel<bool>.Success(true, "Student deleted successfully");

        return ResponseModel<bool>.Failure("Failed to delete student");
    }
}
