using Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Registrations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder
            .HasOne(r => r.Student)
            .WithMany(s => s.Registrations)
            .HasForeignKey(r => r.StudentId);
        
        builder
            .HasOne(r => r.Academy)
            .WithMany(a => a.Registrations)
            .HasForeignKey(r => r.AcademyId);

        builder
            .HasOne(r => r.SchoolYear)
            .WithMany(s => s.Registrations)
            .HasForeignKey(r => r.CurrentSchoolYearId);
        
        builder
            .HasOne(r => r.CurrentClass)
            .WithMany(c => c.Registrations)
            .HasForeignKey(r => r.CurrentClassId);

        builder
            .HasIndex(r => new { r.StudentId, r.AcademyId, r.CurrentSchoolYearId })
            .IsUnique();
    }
}
