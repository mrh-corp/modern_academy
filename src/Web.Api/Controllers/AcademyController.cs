using Application.Academies;
using Domain.Academies;
using Facet.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using OneOf;
using Web.Api.Responses;

namespace Web.Api.Controllers;

[Route("api/academy")]
public class AcademyController(IAcademyRepository academyRepository) : Controller
{
    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<AcademyResponse>> CreateAcademy([FromBody] AcademyDto academyDto)
    {
        OneOf<Error, Academy> result = await academyRepository.CreateAcademy(academyDto);
        return result.Match<ActionResult>(
            error => BadRequest(Result.Failure(error)),
            academy => Ok(Result.Success(academy.ToFacet<Academy, AcademyResponse>())));
    }
}
