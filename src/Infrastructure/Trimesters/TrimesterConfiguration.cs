using Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Trimesters;

public class TrimesterConfiguration : IEntityTypeConfiguration<Trimester>
{
    public void Configure(EntityTypeBuilder<Trimester> builder)
    {
        builder
            .HasOne(t => t.SchoolYear)
            .WithMany(s => s.Trimesters)
            .HasForeignKey(t => t.SchoolYearId);
        
        builder
            .HasIndex(t => new { t.Name, t.SchoolYearId });
    }
}
