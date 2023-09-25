
namespace Parking_Intelligence_Api.Models
{
    public class Tables
    {
        public Tables(decimal car, decimal motorcycle, decimal bus)
        {
            this.car = car;
            this.motorcycle = motorcycle;
            this.bus = bus;
        }

        public int id { get; private set; } 
        public decimal car { get; private set; }
        public decimal motorcycle { get; private set; }
        public decimal bus { get; private set; }
        public virtual Calendars Calendar { get; private set; }
    }
}
