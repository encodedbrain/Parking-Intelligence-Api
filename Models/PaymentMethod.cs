namespace Parking_Intelligence_Api.Models
{
    public class PaymentMethod
    {
        public PaymentMethod(string method)
        {
            this.method = method;
        }
        public int id { get; private set; } 
        public string method { get; private set; }
        public int buy_id { get; private set; } 
        public Buy Buy { get; private set; }
    }
}
