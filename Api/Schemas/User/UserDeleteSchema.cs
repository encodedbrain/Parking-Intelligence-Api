namespace Parking_Intelligence_Api.Schemas.User
{
    public class UserDeleteSchema
    {
        public UserDeleteSchema(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
