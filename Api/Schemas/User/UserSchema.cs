namespace Parking_Intelligence_Api.Schemas.User
{
    public class UserSchema
    {
        public UserSchema(
            string nickname,
            string email,
            string password,
            string address,
            string cpf,
            string fullname,
            string phone
        )
        {
            this.Nickname = nickname;
            this.Email = email;
            this.Password = password;
            this.Address = address;
            this.Cpf = cpf;
            this.Fullname = fullname;
            this.Phone = phone;
        }

        public string Nickname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Cpf { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; private set; }
    }
}
