namespace Parking_Intelligence_Api.Schemas
{
    public class LoginSchema
    {
        public LoginSchema(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string email { get; set; }
        public string password { get; set; }
    }
}
