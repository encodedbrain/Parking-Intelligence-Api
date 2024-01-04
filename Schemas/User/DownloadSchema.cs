namespace Parking_Intelligence_Api.Schemas.User;

public class DownloadSchema
{
    public DownloadSchema(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public DownloadSchema()
    {
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}