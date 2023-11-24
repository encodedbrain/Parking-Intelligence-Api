
using Parking_Intelligence_Api.Schemas.vehicle;

namespace Parking_Intelligence_Api.Schemas.buy
{
    public class BuySchema : VehicleSchema 
    {
        public BuySchema(string model, string color, int year, string brand, string licensePlate, string species,
            string name,  string email, string password, string vacancyType, decimal amountPaid,
            string method) : base(model, color, year, brand, licensePlate, species, name)
        {
       
            Email = email;
            Password = password;
            VacancyType = vacancyType;
            AmountPaid = amountPaid;
            Method = method;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VacancyType { get; set; }
        public decimal AmountPaid { get; set; }
        public string Method { get; set; }
    }
}
