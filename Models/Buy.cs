namespace Parking_Intelligence_Api.Models
{
    public class Buy
    {
        public Buy(decimal value, DateTime date, string vacancyType)
        {
            this.value = value;
            this.date = date;
            this.vacancyType = vacancyType;
        }

        public int id { get; private set; }
        public decimal value { get; private set; }
        public DateTime date { get; private set; }
        public string vacancyType { get; private set; }
        public virtual Invoice Invoice { get; private set; }
        public virtual PaymentMethod PaymentMethod { get; private set; }
        public int user_id { get; private set; }
        public User User { get; private set; }
    }
}
