namespace Parking_Intelligence_Api.Models
{
    public class Ticket
    {
        public Ticket(int ticketNumber, int sequence, DateTime date, string hour)
        {
            TicketNumber = ticketNumber;
            Sequence = sequence;
            Date = date;
            Hour = hour;
        }


        public Ticket()
        {
        }
        public int Id { get; private set; }
        public int TicketNumber { get; internal set; }
        public int Sequence { get; internal set; }
        public DateTime Date { get; internal set; }
        public string Hour { get; internal set; }
        public int InvoiceId { get; private set; }
        public virtual Invoice Invoice { get; private set; }
    }
}
