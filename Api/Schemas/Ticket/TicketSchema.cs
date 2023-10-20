namespace Parking_Intelligence_Api.Schemas.Ticket;

public class TicketSchema
{
    public TicketSchema(string email, string password, string date)
    {
        Email = email;
        Password = password;
        Date = date; ;

    }

    protected TicketSchema()
    {

    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Date { get;  set; } = null!;
}