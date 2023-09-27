namespace Parking_Intelligence_Api.Schemas
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
            this.nickname = nickname;
            this.email = email;
            this.password = password;
            this.address = address;
            this.cpf = cpf;
            this.Fullname = fullname;
            this.phone = phone;
        }

        public string nickname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string cpf { get; set; }
        public string Fullname { get; set; }
        public string phone { get; private set; }
    }
}
