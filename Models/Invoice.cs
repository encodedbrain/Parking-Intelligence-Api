namespace Parking_Intelligence_Api.Models
{
    public class Invoice
    {
        public Invoice(
            int ticketNumber,
            DateTime dateEntry,
            DateTime departureDate,
            DateTime stayTime,
            decimal amountPaid,
            decimal expense,
            decimal change
        )
        {
            this.TicketNumber = ticketNumber;
            this.DateEntry = dateEntry;
            this.DepartureDate = departureDate;
            this.StayTime = stayTime;
            this.AmountPaid = amountPaid;
            this.Expense = expense;
            this.Change = change;
        }

        public Invoice()
        {
        }

        public int Id { get; private set; }
        public int TicketNumber { get; internal set; }
        public DateTime DateEntry { get; internal set; }
        public DateTime DepartureDate { get; internal set; }
        public DateTime StayTime { get; internal set; }
        public decimal AmountPaid { get; internal set; }
        public decimal Expense { get; internal set; }
        public int BuyId { get; private set; }
        public virtual Buy Buy { get; private set; }
        public virtual Ticket Ticket { get; internal set; }
        public decimal Change { get; set; }
    }
}
