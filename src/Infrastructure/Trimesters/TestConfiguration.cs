using Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Trimesters;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder
            .HasOne(t => t.Trimester)
            .WithMany(t => t.Tests)
            .HasForeignKey(t => t.TrimesterId);

        builder
            .HasIndex(t => new { t.Name, t.TrimesterId });
    }
}
