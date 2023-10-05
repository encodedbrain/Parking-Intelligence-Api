using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services;

public class DeleteUserServices
{
    private readonly User _user = new();

    internal async Task<bool> SearchingForUser(LoginSchema prop)
    {
        using (var db = new ParkingDb())
        {
            if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
                return false;

            var userVerify = db.Users.Any(
                user => user.Email == prop.Email && user.Password == _user.EncryptingPassword(prop.Password)
            );

            var foreingkeyUser = db.Users
                .Where(user => user.Password == _user.EncryptingPassword(prop.Password) && user.Email == prop.Email)
                .Select(u => u.UserData.UserId)
                .ToArray();

            var idUser = db.Users.Where(user => user.Id == foreingkeyUser[0]).Select(user => user.Id).ToArray();

            var getUser = db.Users.Find(idUser[0]);

            if (!userVerify || getUser is null)
                return false;

            var dataUser = db.UserDatas.Where(user => user.UserId == idUser[0]);
            var buyUser = db.Buys.Where(buy => buy.UserId == idUser[0]);
            var vehicleUser = db.Vehicles.Where(vehicle => vehicle.UserId == idUser[0]);

            foreach (var data in dataUser) db.UserDatas.Remove(data);
            foreach (var buy in buyUser) db.Buys.Remove(buy);
            foreach (var vehicle in vehicleUser) db.Vehicles.Remove(vehicle);

            db.Users.Remove(getUser);
            await db.SaveChangesAsync();

            return true;
        }
    }
}