namespace Parking_Intelligence_Api.Models
{
    public class User
    {
        public User() { }

        public User(string email, string password)
        {
            var rnd = new Random().Next();
            UserId = rnd;
            // ParkingId = rnd;
        }

        public User(string cpf, int age)
        {
            Cpf = cpf;
            Age = age;
        }

        public int UserId { get; set; }

        public int AccountId { get; private set; }
        public Account Account { get; private set; }
        public string Cpf { get; private set; }
        public int Age { get; private set; }
        public virtual IEnumerable<Shopping>? Shoppings { get; private set; }
    }
}
