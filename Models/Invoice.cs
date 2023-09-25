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
            decimal rest
        )
        {
            this.ticketNumber = ticketNumber;
            this.dateEntry = dateEntry;
            this.departureDate = departureDate;
            this.stayTime = stayTime;
            this.amountPaid = amountPaid;
            this.expense = expense;
            this.rest = rest;
        }
        public int id { get; private set; }
        public int ticketNumber { get; private set; }
        public DateTime dateEntry { get; private set; }
        public DateTime departureDate { get; private set; }
        public DateTime stayTime { get; private set; }
        public decimal amountPaid { get; private set; }
        public decimal expense { get; private set; }
        public decimal rest { get; private set; }
        public int buy_id { get; private set; }
        public virtual Buy Buy { get; private set; }
        public virtual Ticket Ticket { get; private set; }
    }
}
