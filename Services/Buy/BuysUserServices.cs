using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas.buy;

namespace Parking_Intelligence_Api.Services.Buy;

public class BuysUserServices
{
    public object[]? ListingPurchases(GetBuySchema prop)
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users
                .Where(user =>
                    user.Nickname == prop.Name).Select(
                    user => new
                    {
                        User = user,
                        Buy = user.Buys
                    }).FirstOrDefault();


            if (user is null) return null;

            foreach (var buy in user.Buy)
            {
                buy.User = null!;
                return new object[] { buy };
            }

            return null;
        }
    }
}