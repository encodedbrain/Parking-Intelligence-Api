

namespace Parking_Intelligence_Api.Schemas
{
    public class BuySchema : VehicleSchema
    {
        public BuySchema(string model, string color, int year, string brand, string licensePlate, string species, string name, decimal value, string email, string password, string vacancyType, decimal amountPaid, string method) : base(model, color, year, brand, licensePlate, species, name)
        {
            Value = value;
            Email = email;
            Password = password;
            VacancyType = vacancyType;
            AmountPaid = amountPaid;
            Method = method;
        }

        public decimal Value { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VacancyType { get; set; }
        public decimal AmountPaid { get; set; }
        public string Method { get; set; }
    }
}
