using CourseCatalogue.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCatalogue.Infrastructure.Context;

public class CourseCatalogueContext : DbContext
{
    public CourseCatalogueContext(DbContextOptions<CourseCatalogueContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
}