using CourseCatalogue.Application.Dtos;
using CourseCatalogue.Domain;
using CourseCatalogue.Domain.Entities;
using CourseCatalogue.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalogue.Application.Services;

public class CourseService(CourseCatalogueContext context)
{
    public async Task<ResponseModel<Guid>> CreateAsync(CreateCourseDto createCourse)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Title = createCourse.Title,
            Description = createCourse.Description,
            Duration = createCourse.Duration,
            Instructor = createCourse.Instructor
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

        var courseDto = await context.Courses
            .Where(x => x.Id == id)
            .Select(x => new CourseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                Instructor = x.Instructor
            })
            .FirstOrDefaultAsync();

        if (courseDto == null)
        {
            return ResponseModel<CourseDto>.Failure("Failed to get course");
        }

        return ResponseModel<CourseDto>.Success(courseDto, "Course retrieved successfully");
    }

    public async Task<ResponseModel<IEnumerable<CourseDto>>> GetAllAsync()
    {
        var courses = await context.Courses
            .Select(x => new CourseDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Duration = x.Duration,
                Instructor = x.Instructor
            })
            .ToListAsync();

        if (courses.Count > 0)
        {
            return ResponseModel<IEnumerable<CourseDto>>.Success(courses, "Courses retrieved successfully");
        }

        return ResponseModel<IEnumerable<CourseDto>>.Failure("No courses found");
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