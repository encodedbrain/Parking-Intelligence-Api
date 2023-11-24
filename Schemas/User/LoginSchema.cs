namespace Parking_Intelligence_Api.Schemas.User
{
    public class LoginSchema
    {
        public LoginSchema(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public LoginSchema()
        {
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
