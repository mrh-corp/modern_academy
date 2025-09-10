using Domain.Academies;

namespace Application.Abstractions.Params;

public interface IActiveParamsContext
{
    Guid SchoolYearId { get; }
    Task<SchoolYear> ActiveSchoolYear { get; }
    
    Guid AcademyId { get; }
    Task<Academy> ActiveAcademy { get; }
}
