using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Services
{
    public class DeleteUserServices
    {
        ParkingDB DB = new ParkingDB();
        User User = new User();

        internal async Task<bool> SearchingForUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            var userVerify = DB.Users.Any(
                u => u.email == email && u.password == User.EncryptingPassword(password)
            );

            var ForeingkeyUser = DB.Users
                .Where(u => u.password == User.EncryptingPassword(password) && u.email == email)
                .Select(u => u.userData.userId)
                .ToArray();

            var idUser = DB.Users.Where(u => u.id == ForeingkeyUser[0]).Select(u => u.id).ToArray();

            var getUser = DB.Users.Find(idUser[0]);

            if (!userVerify || ForeingkeyUser == null || idUser == null || getUser == null)
                return false;

            var dataUser = DB.UserDatas.Where(u => u.userId == idUser[0]);
            var buyUser = DB.Buys.Where(b => b.userId == idUser[0]);
            var vehicleUser = DB.Vehicles.Where(v => v.userId == idUser[0]);

            foreach (var value in dataUser)
            {
                DB.UserDatas.Remove(value);
            }
            foreach (var value in buyUser)
            {
                DB.Buys.Remove(value);
            }
            foreach (var value in vehicleUser)
            {
                DB.Vehicles.Remove(value);
            }

            DB.Users.Remove(getUser);
            await DB.SaveChangesAsync();

            return true;
        }
    }
}
