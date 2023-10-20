using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_Intelligence_Api.Models
{
    [NotMapped]
    public class Calendars
    {
        public Calendars(DateTime date)
        {
            Date = date;
        }

        public Calendars()
        {

        }

        public int Id { get;  set; }
        public DateTime Date { get; set; }
    }
}
