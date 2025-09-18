using Domain.Academies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Classes;

public class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.HasOne(c => c.PreviousClass)
            .WithOne()
            .HasForeignKey<Class>(c => c.PreviousClassId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(c => c.NextClass)
            .WithOne()
            .HasForeignKey<Class>(c => c.NextClassId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.Academy)
            .WithMany(a => a.Classes)
            .HasForeignKey(c => c.AcademyId);

        builder
            .HasIndex(c => new { c.AcademyId, c.Name })
            .IsUnique();

        builder
            .HasIndex(c => new { c.AcademyId, c.Label })
            .IsUnique();
    }
}
