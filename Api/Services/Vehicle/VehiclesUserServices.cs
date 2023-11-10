using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas.vehicle;

namespace Parking_Intelligence_Api.Services.Vehicle;

public class VehiclesUserServices
{
    public object GetVehicles(GetVehicleSchema prop)
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users
                .Where(user =>
                    user.Nickname == prop.Name)
                .Select(
                    user => new
                    {
                        User = user,
                        Vehicle = user.Vehicles
                    }).FirstOrDefault();


            if (user is null) return false;


            foreach (var vehicle in user.Vehicle)
            {
                if (vehicle.User != null) vehicle.User = null;

                return vehicle;
            }

            return true;
        }
    }
}