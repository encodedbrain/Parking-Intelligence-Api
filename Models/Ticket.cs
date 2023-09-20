namespace Parking_Intelligence_Api.Models
{
    public class Ticket
    {
        public Ticket() { }

        public Ticket(string address)
        {
            var rnd = new Random().Next();

            Id = rnd;
            Number = rnd;
            Sequence = rnd;
            Hour = DateTime.Now.ToShortTimeString();
            Date = DateTime.Now.ToShortDateString();
            Address = address;
        }

        public int Id { get; private set; }
        public int TicketId { get; private set; }
        public int Number { get; private set; }
        public int Sequence { get; private set; }
        public string Hour { get; private set; }
        public string Date { get; private set; }
        public string Address { get; private set; }
        public IEnumerable<Shopping> Shopping { get; private set; }
    }
}
