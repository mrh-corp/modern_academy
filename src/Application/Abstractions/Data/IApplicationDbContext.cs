using Domain.Academies;
using Domain.Todos;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Academy>  Academies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
