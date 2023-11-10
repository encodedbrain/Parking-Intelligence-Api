// using Parking_Intelligence_Api.Data;
// using Parking_Intelligence_Api.Models;
// using Parking_Intelligence_Api.Schemas;
//
// namespace Parking_Intelligence_Api.Services;
//
// public class CreateVehicleServices : VehicleSchema
// {
//     public void RegisterVehicle(UserSchema user) 
//     {
//         using (var db = new ParkingDB())
//         {
//             var getUser = db.Users.FirstOrDefault(u =>
//                 u.email == user.Email && u.password == new User().EncryptingPassword(user.Password));
//
//             if (getUser == null) return;
//
//             getUser.Vehicles = new List<Vehicle>()
//             {
//                 new()
//                 {
//                     species = species,
//                     licensePlate = licensePlate,
//                     color = color,
//                     model = model,
//                     year = year,
//                     brand = brand
//                 }
//             };
//         }
//     }
// }