namespace Parking_Intelligence_Api.Models
{
    public class Price
    {
        public Price()
        {
            var rdn = new Random().Next();
            Id = rdn;
            Car = 20.53M;
            Bus = 40.54M;
            Motorcycle = 15.87M;
            ParkingId = rdn;
        }

        public int Id { get; private set; }
        public decimal Car { get; private set; }
        public decimal Bus { get; private set; }
        public decimal Motorcycle { get; private set; }
        public int ParkingId { get; private set; }
        public virtual Parking? Parkings { get; private set; }
    }
}
