using Application.Abstractions.Authentication;
using Application.Users;
using Domain.Users;
using Facet.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Infrastructure;
using Web.Api.Responses;

namespace Web.Api.Controllers;

[Route("api/user")]
public class UserController(IUserRepository userRepository, ITokenProvider tokenProvider) : Controller
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Result<UserResponse>>> CreateUser(
        [FromBody] CreateUserDto dto)
    {
        Result<User> result = await userRepository.AddUser(dto);
        Result<UserResponse> response = result.MapResponse<User, UserResponse>();
        return Ok(response);
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<ActionResult<Result<SignInResponse>>> SignIn(
        [FromBody] SignInDto dto)
    {
        User? user = await userRepository.GetUserByUsername(dto.Email);
        if (user is null)
        {
            return BadRequest(
                CustomResults.Problem(
                    Result.Failure(Error.NotFound("404", "User not found"))
                ));
        }
        
        bool isPasswordMatched = await userRepository.VerifyIfUserMatchPassword(user, dto.Password);
        if (!isPasswordMatched)
        {
            return BadRequest(
                Result.Failure(Error.Problem("400", "User password doesn't match")));
        }

        string token = tokenProvider.Create(user);
        var response = new SignInResponse(
            token,
            user.ToFacet<User, UserResponse>());
        return Ok(Result.Success(response));
    }
}
