

namespace Parking_Intelligence_Api.Schemas
{
    public class BuySchema
    {
        public BuySchema(decimal value, string email, string password, string vacancyType, decimal amountPaid,
            string method)
        {
            Value = value;
            Email = email;
            Password = password;
            VacancyType = vacancyType;
            AmountPaid = amountPaid;
            Method = method;
        }

        public BuySchema()
        {
        }

        public int Id { get; set; }
        public decimal Value { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VacancyType { get; set; }
        public decimal AmountPaid { get; set; }
        public string Method { get; set; }
    }
}
