using Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Courses;

public class CourseCreditConfiguration : IEntityTypeConfiguration<CourseCredit>
{
    public void Configure(EntityTypeBuilder<CourseCredit> builder)
    {
        builder
            .HasOne(credit => credit.Course)
            .WithMany(course => course.CourseCredits)
            .HasForeignKey(credit => credit.CourseId);
    }
}
