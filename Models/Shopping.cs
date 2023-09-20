namespace Parking_Intelligence_Api.Models
{
    public class Shopping
    {
        public Shopping() { }

        public Shopping(decimal payment, string vacancy, string type)
        {
            int rnd = new Random().Next();
            Payment = payment;
            Vacancy = vacancy;
            Type = type;
            Id = rnd;
            ShoppingId = rnd;
        }

        public int Id { get; private set; }
        public int ShoppingId { get; set; }
        public decimal Payment { get; private set; }
        public string Vacancy { get; private set; }
        public string Type { get; private set; }
        public int UserId { get; private set; }
        public int VehicleId { get; private set; }
        public int TicketId { get; private set; }
        public int InvoiceId { get; private set; }
        public virtual User Users { get; private set; }
        public virtual Vehicle Vehicle { get; private set; }
        public virtual Invoice Invoice { get; private set; }
        public virtual Ticket Ticket { get; private set; }
    }
}
