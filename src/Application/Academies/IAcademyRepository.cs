using Application.Abstractions.Service;
using Domain.Academies;
using SharedKernel;
using OneOf;

namespace Application.Academies;

public interface IAcademyRepository : IService
{
    Task<OneOf<Error, Academy>> CreateAcademy(AcademyDto academyDto);
    Task<List<Academy>> GetAllAcademy();
    
}
