namespace Parking_Intelligence_Api.Schemas.User;

public class UpdatePhotoProfileSchema
{
    public UpdatePhotoProfileSchema(string email, string password, IFormFile image)
    {
        Email = email;
        Password = password;
        Image = image;
    }

    public UpdatePhotoProfileSchema()
    {
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public IFormFile Image { get; set; } = null!;
}