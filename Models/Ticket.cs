namespace Parking_Intelligence_Api.Models
{
    public class Ticket
    {
        public Ticket(int ticketNumber, int sequence, string date, string hour)
        {
            this.ticketNumber = ticketNumber;
            this.sequence = sequence;
            this.date = date;
            this.hour = hour;
        }

        public int id { get; private set; }
        public int ticketNumber { get; private set; }
        public int sequence { get; private set; }
        public string date { get; private set; }
        public string hour { get; private set; }
        public int invoice_id { get; private set; }
        public virtual Invoice Invoice { get; private set; }
    }
}
