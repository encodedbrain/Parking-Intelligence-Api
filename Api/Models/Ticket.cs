namespace Parking_Intelligence_Api.Models
{
    public class Ticket
    {
        public Ticket(int ticketNumber, int sequence, string date, string hour)
        {
            TicketNumber = ticketNumber;
            Sequence = sequence;
            Date = date;
            Hour = hour;
        }


        public Ticket()
        {
        }
        public int Id { get;  set; }
        public int TicketNumber { get; internal set; }
        public int Sequence { get; internal set; }
        public string Date { get; internal set; } = null!;
        public string Hour { get; internal set; } = null!;
        public int BuyId { get;  set; }
        public virtual Buy Buy { get; private set; } = null!;
    }
}
