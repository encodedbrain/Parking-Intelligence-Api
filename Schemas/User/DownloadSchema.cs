namespace Parking_Intelligence_Api.Schemas.User;

public class DownloadSchema
{
    public DownloadSchema(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}