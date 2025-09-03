using Application.Users.Dtos;
using Application.Users.Service;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web.Api.Controllers;

[Route("api/user")]
public class UserController(IUserRepository userRepository) : Controller
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Result<User>>> CreateUser(
        [FromBody] CreateUserDto dto)
    {
        Result<User> result = await userRepository.AddUser(dto);
        return Ok(result);
    }
}
