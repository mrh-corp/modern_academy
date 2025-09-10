namespace Application.Users;

public record CreateUserDto(string Email, string Firstname, string? Lastname, string Password);
public record SignInDto(string Email, string Password);
