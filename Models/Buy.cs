namespace Parking_Intelligence_Api.Models
{
    public class Buy
    {
        public Buy(decimal value, DateTime date, string vacancyType, Invoice invoice, PaymentMethod paymentMethod)
        {
            this.value = value;
            this.date = date;
            this.vacancyType = vacancyType;
            this.invoice = invoice;
            this.paymentMethod = paymentMethod;
        }

        public Buy()
        {
            
        }

        public int id { get; private set; }
        public decimal value { get; internal set; }
        public DateTime date { get; internal set; }
        public string vacancyType { get; internal set; }
        public virtual Invoice invoice { get; internal set; }
        public virtual PaymentMethod paymentMethod { get; internal set; }
        public int userId { get; internal set; }
        public User User { get; internal set; }
    }
}