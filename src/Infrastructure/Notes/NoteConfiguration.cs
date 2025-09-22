using Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Notes;

public class NoteConfiguration: IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder
            .HasOne(n => n.Test)
            .WithMany(t => t.Notes)
            .HasForeignKey(n => n.TestId);
        
        builder
            .HasOne(n => n.Course)
            .WithMany(c => c.Notes)
            .HasForeignKey(n => n.CourseId);
        
        builder
            .HasOne(n => n.Student)
            .WithMany(s => s.Notes)
            .HasForeignKey(n => n.StudentId);
        
        builder
            .HasIndex(n => new { n.StudentId, n.CourseId, n.TestId })
            .IsUnique();
    }
}
