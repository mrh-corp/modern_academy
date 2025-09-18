using Domain.Academies;
using Domain.Courses;
using Domain.Registrations;
using Domain.Students;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Academy>  Academies { get; }
    DbSet<SchoolYear>  SchoolYears { get; }
    DbSet<Class> Classes { get; }
    DbSet<Course> Courses { get; }
    DbSet<CourseCredit> CourseCredits { get; }
    DbSet<ClassCourse> ClassCourses { get; }
    DbSet<Student>  Students { get; }
    DbSet<Registration>  Registrations { get; }
    
    DatabaseFacade  Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
