namespace Parking_Intelligence_Api.Models
{
    public class Calendars
    {
        public Calendars(DayOfWeek day, DateTime mounth, DateTime year)
        {
            this.day = day;
            this.mounth = mounth;
            this.year = year;
        }

        public int id { get; private set; }
        public DayOfWeek day { get; private set; }
        public DateTime mounth { get; private set; }
        public DateTime year { get; private set; }
        public int tables_id { get; private set; }
        public virtual Tables Table { get; private set; }
    }
}
