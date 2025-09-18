using Application.Abstractions.Data;
using Application.Academies;
using Domain.Academies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Classes;

public class ClassService(IApplicationDbContext context) : IClassRepository
{
    public async Task<Class> GetClassById(Guid id)
    {
        return await context.Classes.SingleOrDefaultAsync(x => x.Id == id);
    }
}
