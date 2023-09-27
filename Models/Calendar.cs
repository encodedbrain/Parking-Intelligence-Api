namespace Parking_Intelligence_Api.Models
{
    public class Calendars
    {
        public Calendars()
        {

        }

        public int id { get; private set; }
        public DateTime date { get; set; }
        public int tables_id { get; private set; }
        public virtual Tables Table { get; private set; }
    }
}
