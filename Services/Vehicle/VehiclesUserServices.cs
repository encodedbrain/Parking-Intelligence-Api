using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services;

public class VehiclesUserServices
{
    public object[]? GetVehicles(LoginSchema prop)
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users
                .Where(user =>
                    user.Email == prop.Email && user.Password == new User().EncryptingPassword(prop.Password)).Select(
                    user => new
                    {
                        Vehicle = user.Vehicles
                    }).FirstOrDefault();


            if (user is null) return null;

            foreach (var vehicle in user.Vehicle)
            {
                vehicle.User.Password = string.Empty;
                return new object[] { vehicle };
            }

            return null;
        }
    }
}