// using Parking_Intelligence_Api.Data;
// using Parking_Intelligence_Api.Schemas.vehicle;
//
// namespace Parking_Intelligence_Api.Services.Vehicle;
//
// public class DeleteVehiclesServices
// {
//     public async Task<bool> DeleteVehiclesService(DeleteVehicleSchema prop)
//     {
//         using (var db = new ParkingDb())
//         {
//             var user = db.Users.FirstOrDefault(user => user.Email == prop.Email
//                                                        && user.Password == prop.Password);
//
//             if (user is null) return false;
//             var vehicle = db.Vehicles.Where(vehicle =>
//                 vehicle.LicensePlate == prop.LicensePlate && vehicle.UserId == user.Id);
//
//
//             var buy = db.Buys.Where(buy => buy.VehicleIdentifier == prop.LicensePlate && buy.UserId == user.Id);
//
//             foreach (var value in vehicle) db.Vehicles.Remove(value);
//
//             foreach (var value in buy) db.Buys.Remove(value);
//
//
//             await db.SaveChangesAsync();
//
//             return true;
//         }
//     }
// }