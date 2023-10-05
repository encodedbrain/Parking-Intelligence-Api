namespace Parking_Intelligence_Api.Models
{
    public class PaymentMethod
    {


        public PaymentMethod()
        {
        }


        public PaymentMethod(string method)
        {
            Method = method;
        }
        
        public int Id { get; private set; }
        public string Method { get; internal set; }
        public int BuyId { get; private set; } 
        public Buy Buy { get; private set; }
    }
}
