using Parking_Intelligence_Api.Schemas.buy;
using Parking_Intelligence_Api.Schemas.User;

namespace Parking_Intelligence_Api.interfaces;

public interface IBuy
{
    public bool Purchase(BuySchema prop);
    public object[]? GetPurchases(GetBuySchema prop);

    public bool DeleteBuy(UserDeleteSchema prop);

}