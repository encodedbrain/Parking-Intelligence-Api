namespace Parking_Intelligence_Api.Schemas
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

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
