using Parking_Intelligence_Api.interfaces;

namespace Parking_Intelligence_Api.Models
{
    public class UserData : IDataUser
    {
        public UserData() { }

        public UserData(string? fullName, string? cpf, string? address, string? phone)
        {
            this.fullName = fullName;
            cpf = cpf;
            this.address = address;
            this.phone = phone;
        }

        public int id { get; set; }
        public string? fullName { get; internal set; }
        public string? cpf { get; internal set; }
        public string? address { get; internal set; }
        public string? phone { get; internal set; }
        public int userId { get; internal set; }
        public User User { get; internal set; }
    }
}
