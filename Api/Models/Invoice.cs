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
        decimal change
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

    public int Id { get; set; }
    public int TicketNumber { get; internal set; }
    public  string DateEntry { get; internal set; } = null!;
    public DateTime DepartureDate { get; internal set; }
    public string StayTime { get; internal set; } = null!;
    public decimal AmountPaid { get; internal set; }
    public decimal Expense { get; internal set; }
    public string LimitTime { get; set; } = null!;
    public int BuyId { get;  set; }
    public virtual Buy Buy { get; private set; } = null!;
    public decimal Change { get; set; }
}