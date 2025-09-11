using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Domain.Academies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Params;

public class ActiveParamsContext(IApplicationDbContext context) :  IActiveParamsContext
{
    public Guid SchoolYearId { get; set; }
    public Task<SchoolYear> ActiveSchoolYear => GetActiveSchoolYear();
    public Guid AcademyId { get; set; }
    public Task<Academy> ActiveAcademy => GetActiveAcademy();

    private async Task<SchoolYear> GetActiveSchoolYear()
    {
        SchoolYear schoolYear = await context.SchoolYears.SingleOrDefaultAsync(s => s.Id == SchoolYearId);
        return schoolYear;
    }

    private async Task<Academy> GetActiveAcademy()
    {
        Academy academy = await context.Academies.SingleOrDefaultAsync(a => a.Id == AcademyId);
        return academy;
    }
}
