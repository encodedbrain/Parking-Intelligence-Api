namespace Parking_Intelligence_Api.Models
{
    public sealed class Buy
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

        public int Id { get; set; }
        public decimal Value { get; internal set; }

        public string VehicleIdentifier { get; set; } = null!;
        public string Date { get; internal set; } = null!;
        public string VacancyType { get; internal set; } = null!;
        public Invoice Invoice { get; internal set; } = null!;
        public PaymentMethod PaymentMethod { get; internal set; } = null!;

        public Ticket Ticket { get; set; } = null!;
        public int UserId { get; internal set; }
        public User User { get; internal set; } = null!;


        public decimal InformsTheValueOfTheVacancy(string type)
        {
            string passenger = "passenger";
            string mixed = "mixed";


            var table = new[]
            {
                20.00,
                40.00,
                70.00
            };

            if (type == passenger) return new decimal(table[0]);
            if (type == mixed) return new decimal(table[1]);
            else return new decimal(table[2]);
        }
    }
}