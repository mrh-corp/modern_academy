namespace Application.Users.Dtos;

public record CreateUserDto(string email, string password);
public record SignInDto(string email, string password);
