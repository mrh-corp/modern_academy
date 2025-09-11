using Domain.Academies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Academies;

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
    }
}
