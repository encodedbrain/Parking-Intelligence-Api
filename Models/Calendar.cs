using System.ComponentModel.DataAnnotations.Schema;

namespace Parking_Intelligence_Api.Models
{
    [NotMapped]
    public class Calendars
    {
        public Calendars()
        {

        }

        public int Id { get; private set; }
        public DateTime Date { get; set; }
    }
}
