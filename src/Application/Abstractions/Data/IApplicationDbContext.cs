using Domain.Academies;
using Domain.Todos;
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
    
    DatabaseFacade  Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
