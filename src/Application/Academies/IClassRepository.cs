using Application.Abstractions.Service;
using Domain.Academies;

namespace Application.Academies;

public interface IClassRepository : IService
{
    Task<Class> GetClassById(Guid id);
}
