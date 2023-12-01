namespace Parking_Intelligence_Api.Schemas.User;

public class UpdatePasswordSchema
{
    public UpdatePasswordSchema(string email, string password, string newPassword)
    {
        Email = email;
        Password = password;
        NewPassword = newPassword;
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
}