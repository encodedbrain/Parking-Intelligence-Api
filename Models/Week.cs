namespace Parking_Intelligence_Api.Models
{
    public class Week
    {
        public Week(int mounth, int day)
        {
            var rnd = new Random().Next();
            Id = rnd;
            int Year = DateTime.Now.Year;
            Day_Week = DateTime.Today.DayOfWeek;
            Mounth = new DateTime(Year, mounth, day).ToString("MMMM");
            ParkingId = rnd;
        }

        public Week()
        {
            bool Day = DateTime.Now.DayOfWeek.ToString() != "Sunday";
            int Hours = DateTime.Now.Hour;

            if (Hours > 18 || Hours < 7 && Day)
                Status = "closed";
            else
                Status = "Open";
        }

        public int Id { get; private set; }
        public DayOfWeek Day_Week { get; private set; }
        public string Mounth { get; private set; }
        public string Status { get; private set; }
        public int ParkingId { get; private set; }
        public virtual Parking Parkings { get; private set; }
    }
}
