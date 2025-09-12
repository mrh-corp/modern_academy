using Application.Academies;
using Domain.Academies;
using Facet.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using OneOf;
using Web.Api.Infrastructure;
using Web.Api.Responses;

namespace Web.Api.Controllers;

[Route("api/academy")]
public class AcademyController(IAcademyRepository academyRepository) : Controller
{
    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<AcademyResponse>> CreateAcademy([FromBody] AcademyDto academyDto, CancellationToken cancellationToken)
    {
        OneOf<Error, Academy> result = await academyRepository.CreateAcademy(academyDto, cancellationToken);
        return result.Match<ActionResult>(
            error => BadRequest(Result.Failure(error)),
            academy => Ok(Result.Success(academy.ToFacet<Academy, AcademyResponse>()))
            );
    }

    [HttpPost("upload-logo")]
    [Authorize]
    public async Task<ActionResult<AcademyResponse>> UploadAcademyLogo([FromForm] AcademyLogo academyLogo, CancellationToken cancellationToken)
    {
        string filename = academyLogo.AcademyLogoFile.FileName;
        Stream fileStream = academyLogo.AcademyLogoFile.OpenReadStream();
        OneOf<Error, Academy>  result = await academyRepository.UploadAcademyLogo(filename, fileStream, cancellationToken);
        return result.Match<ActionResult>(
            error => BadRequest(Result.Failure(error)),
            academy => Ok(Result.Success(academy.ToFacet<Academy, AcademyResponse>()))
        );
    }

    [HttpPost("school-year")]
    [Authorize]
    public async Task<ActionResult<List<SchoolYearResponse>>> CreateSchoolYear([FromBody] SchoolYearDto schoolYearDto,
        CancellationToken token)
    {
        OneOf<Error, List<SchoolYear>> result = await academyRepository.CreateSchoolYear(schoolYearDto, token);
        return result.Match<ActionResult>(
            error =>
            {
                var errorResult =  Result.Failure(error);
                return error.Type switch
                {
                    ErrorType.NotFound => NotFound(errorResult),
                    ErrorType.Forbidden => StatusCode(StatusCodes.Status403Forbidden, errorResult),
                    _ => BadRequest(errorResult)
                };
            }, schoolYears => Ok(Result.Success(schoolYears.SelectFacets<SchoolYear, SchoolYearResponse>().ToList()))
            );
    }

    [HttpGet("{academyId}/school-year")]
    [Authorize]
    public async Task<ActionResult<List<SchoolYearResponse>>> GetSchoolYearList(Guid academyId, CancellationToken token)
    {
        OneOf<Error, List<SchoolYear>> result = await academyRepository.GetAllSchoolYear(academyId, token);
        return result.Match<ActionResult>(
            error =>
            {
                if (error.Type == ErrorType.NotFound)
                {
                    return NotFound(Result.Failure(error));
                }
                return BadRequest(Result.Failure(error));
            }, schoolYears => Ok(Result.Success(schoolYears.SelectFacets<SchoolYear, SchoolYearResponse>().ToList()))
        );
    }

    [HttpPost("classes")]
    [Authorize]
    public async Task<ActionResult<List<ClassResponse>>> CreateClasses([FromBody] ClassDto[] classesDto, CancellationToken token)
    {
        OneOf<Error, List<Class>> result = await academyRepository.AddClasses(classesDto, token);
        return result.Match<ActionResult>(
            error => BadRequest(Result.Failure(error)),
            classes => Ok(Result.Success(classes.SelectFacets<Class, ClassResponse>().ToList())));
    }
}
