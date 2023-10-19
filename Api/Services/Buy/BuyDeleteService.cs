using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services.Buy;

public class BuyDeleteService
{
    public async Task<bool> BuyDelete(UserDeleteSchema prop)
    {
        var user1 = new Models.User();

        if (!user1.ValidatePassword(prop.Password)) return false;
        if (!user1.VaLidateEmail(prop.Email)) return false;


        using (var db = new ParkingDb())
        {
            var user = db.Users.FirstOrDefault(user => user.Email == prop.Email
                                                       && user.Password == user1.EncryptingPassword(prop.Password));

            if (user is null) return false;

            var buy = db.Buys.Where(buy => buy.UserId == user.Id);
            var vehicle = db.Vehicles.Where(vehicle => vehicle.UserId == user.Id);

            foreach (var value in buy) db.Buys.Remove(value);

            foreach (var value in vehicle) db.Vehicles.Remove(value);

            await db.SaveChangesAsync();
        }

        return true;
    }
}