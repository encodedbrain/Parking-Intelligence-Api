namespace Parking_Intelligence_Api.Schemas
{
    public class UserDeleteSchema
    {
        public UserDeleteSchema(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string email { get; set; }
        public string password { get; set; }
    }
}
