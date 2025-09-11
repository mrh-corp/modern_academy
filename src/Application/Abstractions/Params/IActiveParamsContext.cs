using Application.Abstractions.Service;
using Domain.Academies;

namespace Application.Abstractions.Params;

public interface IActiveParamsContext : IService
{
    Guid SchoolYearId { get; set; }
    Task<SchoolYear> ActiveSchoolYear { get; }
    
    Guid AcademyId { get; set; }
    Task<Academy> ActiveAcademy { get; }
}
