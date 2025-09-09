namespace Web.Api.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string? Lastname { get; set; }
    public string Email { get; set; }
}
