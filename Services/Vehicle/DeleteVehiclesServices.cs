using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services;

public class DeleteVehiclesServices 
{
    public async Task<bool> DeleteVehiclesService(DeleteVehicleSchema prop)
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users.FirstOrDefault(user => user.Email == prop.Email
                                                       && user.Password == prop.Password);


            var vehicle = db.Vehicles.FirstOrDefault(vehicle => vehicle.LicensePlate == prop.LicensePlate);


            var buy = db.Buys.FirstOrDefault(buy => buy.VehicleIdentifier == prop.LicensePlate);


            if (vehicle is null || buy is null) return false;


            db.Vehicles.Remove(vehicle);
            db.Buys.Remove(buy);

            await db.SaveChangesAsync();

            return true;
        }
    }
}