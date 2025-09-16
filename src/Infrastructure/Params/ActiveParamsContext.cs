using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Domain.Academies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Params;

public class ActiveParamsContext(IApplicationDbContext context) :  IActiveParamsContext
{
    public Guid SchoolYearId { get; set; }
    public Task<SchoolYear> ActiveSchoolYear => GetActiveSchoolYear();

    private async Task<SchoolYear> GetActiveSchoolYear()
    {
        SchoolYear schoolYear = await context.SchoolYears.SingleOrDefaultAsync(s => s.Id == SchoolYearId);
        return schoolYear;
    }
}
