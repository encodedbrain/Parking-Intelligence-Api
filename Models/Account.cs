namespace Parking_Intelligence_Api.Models
{
    public class Account
    {
        public Account(string nickname, string email, string password)
        {
            var rnd = new Random().Next();
            AccountId = rnd;
            Nickname = nickname;
            Email = email;
            Password = password;
        }

        public int AccountId { get; private set; }
        public string Nickname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public virtual User User { get; private set; }
        public int ParkingId { get; private set; }
        public virtual Parking Parkings { get; private set; }
    }
}
