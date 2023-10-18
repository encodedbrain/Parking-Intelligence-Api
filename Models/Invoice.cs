namespace Parking_Intelligence_Api.Models;

public class Invoice
{
    public Invoice(
        int ticketNumber,
        string dateEntry,
        DateTime departureDate,
        string stayTime,
        decimal amountPaid,
        decimal expense,
        string limitTime,
        decimal change,
        string datelimit
    )
    {
        this.TicketNumber = ticketNumber;
        this.DateEntry = dateEntry;
        this.DepartureDate = departureDate;
        this.StayTime = stayTime;
        this.AmountPaid = amountPaid;
        this.Expense = expense;
        this.LimitTime = limitTime;
        this.Change = change;
    }

    public Invoice()
    {
    }

    public int Id { get; private set; }
    public int TicketNumber { get; internal set; }
    public  string DateEntry { get; internal set; }
    public DateTime DepartureDate { get; internal set; }
    public string StayTime { get; internal set; }
    public decimal AmountPaid { get; internal set; }
    public decimal Expense { get; internal set; }
    public string LimitTime { get; set; }
    public int BuyId { get; private set; }
    public virtual Buy Buy { get; private set; }
    public virtual Ticket Ticket { get; internal set; }
    public decimal Change { get; set; }
}