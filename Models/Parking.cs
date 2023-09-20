namespace Parking_Intelligence_Api.Models
{
    public class Parking
    {
        public Parking()
        {
            var rnd = new Random().Next();
            Id = rnd;
            ParkingId =  rnd;
        }

        public int Id { get; private set; }
        public int ParkingId { get; private set; }
        public virtual IEnumerable<Vehicle> Vehicles { get; private set; }
        public virtual IEnumerable<Week> Weeks { get; private set; }
        public virtual IEnumerable<Account> Accounts { get; private set; }
        public virtual IEnumerable<Price> Prices { get; private set; }
    }
}
