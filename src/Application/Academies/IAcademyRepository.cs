using Application.Abstractions.Service;
using Domain.Academies;
using SharedKernel;
using OneOf;

namespace Application.Academies;

public interface IAcademyRepository : IService
{
    Task<OneOf<Error, Academy>> CreateAcademy(AcademyDto academyDto, CancellationToken cancellationToken = default);
    Task<List<Academy>> GetAllAcademy();
    Task<OneOf<Error, List<SchoolYear>>> CreateSchoolYear(SchoolYearDto schoolYearDto, CancellationToken cancellationToken = default);
    Task<OneOf<Error, List<SchoolYear>>> GetAllSchoolYear(Guid academyId, CancellationToken cancellationToken = default);
    Task<OneOf<Error,List<Class>>> AddClasses(ClassDto[] classes, CancellationToken cancellationToken = default);

}
