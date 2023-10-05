using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services;

public class BuysUserServices
{
    public object[]? ListingPurchases(LoginSchema prop)
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users
                .Where(user =>
                    user.Email == prop.Email && user.Password == new User().EncryptingPassword(prop.Password)).Select(
                    user => new
                    {
                        User = user,
                        Buy = user.Buys
                    }).FirstOrDefault();


            if (user is null) return null;

            foreach (var buy in user.Buy)
            {
                return new object[] { buy };
            }

            return null;
        }
    }
}