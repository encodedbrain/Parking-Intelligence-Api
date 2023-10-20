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
        
        public int Id { get; set; }
        public string Method { get; internal set; } = null!;
        public int BuyId { get; set; } 
        public Buy Buy { get; private set; } = null!;
    }
}
