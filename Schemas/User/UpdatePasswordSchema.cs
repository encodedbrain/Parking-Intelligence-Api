namespace Parking_Intelligence_Api.Schemas.User;

public class UpdatePasswordSchema
{
    public UpdatePasswordSchema(string email, string password, string newPassword)
    {
        Email = email;
        Password = password;
        NewPassword = newPassword;
    }

    public UpdatePasswordSchema()
    {
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}