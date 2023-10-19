namespace Parking_Intelligence_Api.Models
{
    public class Buy
    {
        public Buy(decimal value, string date, string vacancyType, Invoice invoice, PaymentMethod paymentMethod)
        {
            Value = value;
            Date = date;
            VacancyType = vacancyType;
            Invoice = invoice;
            PaymentMethod = paymentMethod;
        }

        public Buy()
        {
        }

        public Buy(string vehicleIdentifier)
        {
            VehicleIdentifier = vehicleIdentifier;
        }

        public int Id { get; private set; }
        public decimal Value { get; internal set; }

        public string VehicleIdentifier { get; set; }
        public string Date { get; internal set; }
        public string VacancyType { get; internal set; }
        public virtual Invoice Invoice { get; internal set; }
        public virtual PaymentMethod PaymentMethod { get; internal set; }
        public int UserId { get; internal set; }
        public User User { get; internal set; }
        
    }
}