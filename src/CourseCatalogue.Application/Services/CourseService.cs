using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Application.Interfaces;
using CourseCatalogue.Domain;
using CourseCatalogue.Domain.Entities;
using CourseCatalogue.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalogue.Application.Services;

public class CourseService(CourseCatalogueContext context) : ICourseService
{
    public async Task<ResponseModel<Guid>> CreateAsync(CreateCourseDto createCourse)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = createCourse.Title,
            Description = createCourse.Description,
            Duration = createCourse.Duration,
            Instructor = createCourse.Instructor,
            CreatedAt = DateTime.UtcNow
        };

        await context.Courses.AddAsync(course);

        if (await context.SaveChangesAsync() > 0)
        {
            return ResponseModel<Guid>.Success(course.Id, "Course created successfully");
        }

        return ResponseModel<Guid>.Failure("Failed to create course");
    }

    public async Task<ResponseModel<bool>> DeleteAsync(Guid id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
        {
            return ResponseModel<bool>.Success(false, "Course not found");
        }

        context.Courses.Remove(course);

        if (await context.SaveChangesAsync() > 0)
        {
            return ResponseModel<bool>.Success(true, "Course deleted successfully");
        }

        return ResponseModel<bool>.Failure("Failed to delete course");
    }

    public async Task<ResponseModel<CourseDto>> GetByIdAsync(Guid id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
        {
            return ResponseModel<CourseDto>.Success(new CourseDto(), "Course not found");
        }

        var courseDto = new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Duration = course.Duration,
            Instructor = course.Instructor
        };

        return ResponseModel<CourseDto>.Success(courseDto, "Course retrieved successfully");
    }

    public async Task<ResponseModel<IEnumerable<CourseDto>>> GetAllAsync(string? title, string? instructor, int page, int pageSize)
    {
        var query = context.Courses.AsQueryable();
        if (!string.IsNullOrEmpty(title))
            query = query.Where(c => c.Title.Contains(title));
        if (!string.IsNullOrEmpty(instructor))
            query = query.Where(c => c.Instructor.Contains(instructor));

        var courses = await query
            .OrderBy(c => c.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new CourseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                Instructor = x.Instructor
            })
            .ToListAsync();

        return courses.Any()
            ? ResponseModel<IEnumerable<CourseDto>>.Success(courses, "Courses retrieved successfully")
            : ResponseModel<IEnumerable<CourseDto>>.Failure("No courses found");
    }

    public async Task<ResponseModel<bool>> UpdateAsync(UpdateCourseDto updateCourse)
    {
        if (updateCourse == null)
        {
            return ResponseModel<bool>.Failure("Invalid request");
        }

        var course = await context.Courses.FindAsync(updateCourse.Id);
        if (course == null)
        {
            return ResponseModel<bool>.Success(false, "Course not found");
        }

        course.Title = updateCourse.Title;
        course.Description = updateCourse.Description;
        course.Duration = updateCourse.Duration;
        course.Instructor = updateCourse.Instructor;

        context.Courses.Update(course);

        if (await context.SaveChangesAsync() > 0)
        {
            return ResponseModel<bool>.Success(true, "Course updated successfully");
        }

        return ResponseModel<bool>.Failure("Failed to update course");
    }
}