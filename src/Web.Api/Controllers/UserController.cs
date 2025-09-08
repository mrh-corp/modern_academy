using Application.Users.Dtos;
using Application.Users.Service;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Infrastructure;

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

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<User>>> SignIn(
        [FromBody] SignInDto dto)
    {
        User? user = await userRepository.GetUserByUsername(dto.email);
        if (user is null)
        {
            return BadRequest(
                CustomResults.Problem(
                    Result.Failure(Error.NotFound("404", "User not found"))
                ));
        }
        
        bool isPasswordMatched = await userRepository.VerifyIfUserMatchPassword(user, dto.password);
        if (!isPasswordMatched)
        {
            return BadRequest(
                Result.Failure(Error.Problem("400", "User password doesn't match")));
        }
        return Ok(Result.Success(user));
    }
}
