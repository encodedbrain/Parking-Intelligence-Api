namespace Parking_Intelligence_Api.Models
{
    public class Invoice
    {
        public Invoice() { }

        public Invoice(
            string adress,
            int payment,
            int value,
            string type,
            int money,
            int transshipment
        )
        {
            var rnd = new Random().Next();
            Id = rnd;
            Address = adress;
            Ticket = rnd;
            Entrance = DateTime.Now.ToString();
            Payment = payment;
            Value = value;
            Type = type;
            Permanence = DateTime.Now.ToShortTimeString();
            Money = money;
            Transshipment = transshipment;
            InvoiceId = rnd;
        }

        public int Id { get; set; }

        public int InvoiceId { get; private set; }
        public string Address { get; private set; }
        public int Ticket { get; private set; }
        public string Entrance { get; private set; }
        public decimal Payment { get; private set; }
        public decimal Value { get; private set; }
        public string Type { get; private set; }
        public string Permanence { get; private set; }
        public decimal Money { get; private set; }
        public decimal Transshipment { get; private set; }
        public IEnumerable<Shopping> Shopping { get; private set; }
    }
}
