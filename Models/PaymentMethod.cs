namespace Parking_Intelligence_Api.Models
{
    public class PaymentMethod
    {
        public PaymentMethod(string method)
        {
            method = method;
        }

        public PaymentMethod()
        {
        }

        public int id { get; private set; }
        public string method { get; internal set; }
        public int buyId { get; private set; } 
        public Buy Buy { get; private set; }
    }
}
