using Application.Registrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Infrastructure;

namespace Web.Api.Controllers;
[Route("api/registration")]
[ApiController]
[TenantRequired]
[ActiveParamsRequired]
public class RegistrationController() : ControllerBase
{
    [HttpGet("create/")]
    public Task<ActionResult> CreateRegistration([FromBody] RegisterStudentDto registerStudentDto)
    {

    }
}
