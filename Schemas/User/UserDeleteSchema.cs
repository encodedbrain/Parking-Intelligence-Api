namespace Parking_Intelligence_Api.Schemas.User
{
    public class UserDeleteSchema
    {
        public UserDeleteSchema(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public UserDeleteSchema()
        {
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
