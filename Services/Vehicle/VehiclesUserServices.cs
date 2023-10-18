using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services.Vehicle;

public class VehiclesUserServices
{

    public object GetVehicles(LoginSchema prop)
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users
                .Where(user =>
                    user.Email == prop.Email && user.Password == new Models.User().EncryptingPassword(prop.Password) &&
                    user.Id == 1).Select(
                    user => new
                    {
                        User = user,
                        Vehicle = user.Vehicles
                    }).FirstOrDefault();


            if (user is null) return false;


            foreach (var vehicle in user.Vehicle)
            {
                vehicle.User.Password = string.Empty;
                return vehicle;
            }

            return true;
        }
    }
}