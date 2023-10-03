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
            this.ticketNumber = ticketNumber;
            this.dateEntry = dateEntry;
            this.departureDate = departureDate;
            this.stayTime = stayTime;
            this.amountPaid = amountPaid;
            this.expense = expense;
            this.Change = change;
        }

        public Invoice()
        {
        }

        public int id { get; private set; }
        public int ticketNumber { get; internal set; }
        public DateTime dateEntry { get; internal set; }
        public DateTime departureDate { get; internal set; }
        public DateTime stayTime { get; internal set; }
        public decimal amountPaid { get; internal set; }
        public decimal expense { get; internal set; }
        public int buyId { get; private set; }
        public virtual Buy Buy { get; private set; }
        public virtual Ticket Ticket { get; internal set; }
        public decimal Change { get; set; }
    }
}
