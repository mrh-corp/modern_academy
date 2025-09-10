namespace Web.Api.Responses;

public record SignInResponse(string Token, UserResponse User);
