
namespace Parking_Intelligence_Api.Models
{
    public class Tables
    {
        public int id { get; private set; }

        public Tables(decimal car, decimal motorcycle, decimal bus)
        {
            car = car;
            motorcycle = motorcycle;
            bus = bus;
        }

        public decimal car { get; private set; }
        public decimal motorcycle { get; private set; }
        public decimal bus { get; private set; }
        public virtual Calendars Calendar { get; private set; }
    }
}
