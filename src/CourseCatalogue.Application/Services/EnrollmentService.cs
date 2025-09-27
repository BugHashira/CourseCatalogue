using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Application.Interfaces;
using CourseCatalogue.Domain;
using CourseCatalogue.Domain.Entities;
using CourseCatalogue.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalogue.Application.Services;

public class EnrollmentService(CourseCatalogueContext context) : IEnrollmentService
{
    public async Task<ResponseModel<bool>> EnrollAsync(Guid studentId, Guid courseId)
    {
        if (!await context.Students.AnyAsync(s => s.Id == studentId))
            return ResponseModel<bool>.Failure("Student not found.");
        if (!await context.Courses.AnyAsync(c => c.Id == courseId))
            return ResponseModel<bool>.Failure("Course not found.");
        if (await context.Enrollments.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId))
            return ResponseModel<bool>.Failure("Student already enrolled.");

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrollmentDate = DateTime.UtcNow
        };

        await context.Enrollments.AddAsync(enrollment);
        if (await context.SaveChangesAsync() > 0)
            return ResponseModel<bool>.Success(true, "Enrollment successful");

        return ResponseModel<bool>.Failure("Failed to enroll student");
    }

    public async Task<ResponseModel<IEnumerable<CourseDto>>> GetEnrolledCoursesAsync(Guid studentId)
    {
        if (!await context.Students.AnyAsync(s => s.Id == studentId))
            return ResponseModel<IEnumerable<CourseDto>>.Failure("Student not found.");

        var courses = await context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Include(e => e.Course)
            .Select(e => new CourseDto
            {
                Id = e.Course.Id,
                Title = e.Course.Title,
                Description = e.Course.Description,
                Duration = e.Course.Duration,
                Instructor = e.Course.Instructor
            })
            .ToListAsync();

        return courses.Any()
            ? ResponseModel<IEnumerable<CourseDto>>.Success(courses, "Enrolled courses retrieved successfully")
            : ResponseModel<IEnumerable<CourseDto>>.Failure("No enrolled courses found");
    }
}
