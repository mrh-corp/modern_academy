using Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Courses;

public class CourseConfiguration : IEntityTypeConfiguration<ClassCourse>
{
    public void Configure(EntityTypeBuilder<ClassCourse> builder)
    {
        builder
            .HasOne(cc => cc.SchoolYear)
            .WithMany(s => s.ClassCourses)
            .HasForeignKey(cc => cc.SchoolYearId);
        
        builder
            .HasOne(cc => cc.Class)
            .WithMany(c => c.Courses)
            .HasForeignKey(cc => cc.ClassId);

        builder
            .HasMany(c => c.Courses)
            .WithMany(c => c.ClassCourses);
    }
}
