namespace Parking_Intelligence_Api.Schemas.User
{
    public class UserSchema
    {
        public UserSchema(string nickname, string email, string password, string address, string cpf, string fullname,
            string phone)
        {
            Nickname = nickname;
            Email = email;
            Password = password;
            Address = address;
            Cpf = cpf;
            Fullname = fullname;
            Phone = phone;
            // Image = image;
        }

        public UserSchema()
        {
        }

        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
