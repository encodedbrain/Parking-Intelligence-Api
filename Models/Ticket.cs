namespace Parking_Intelligence_Api.Models
{
    public class Ticket
    {
        public int id { get; private set; }

        public Ticket(int ticketNumber, int sequence, DateTime date, string hour)
        {
            ticketNumber = ticketNumber;
            sequence = sequence;
            date = date;
            hour = hour;
        }

        public Ticket()
        {
        }

        public int ticketNumber { get; internal set; }
        public int sequence { get; internal set; }
        public DateTime date { get; internal set; }
        public string hour { get; internal set; }
        public int invoiceId { get; private set; }
        public virtual Invoice Invoice { get; private set; }
    }
}
