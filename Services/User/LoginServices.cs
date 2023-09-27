using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services
{
    public class LoginServices
    {
        ParkingDB DB = new ParkingDB();
        User users = new User();

        internal object ReturnUser(string? email, string? password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return $"{false} -  email ou password vazios";
            var userVerify = DB.Users.Any(
                u => u.email == email && u.password == users.EncryptingPassword(password)
            );
            var user = DB.Users.SingleOrDefault(
                u => u.email == email && u.password == users.EncryptingPassword(password)
            );

            if (!userVerify && user == null)
                return $"{false} -  user null ou userverify";

            if (user != null)
            {
                var token = ParkingServices.GenerateToken(user);
                user.password = string.Empty;

                return new { user, token };
            }
            return true;
        }

        internal bool ValidateCredentials(string email, string password, LoginSchema user)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;
            if (!users.ValidatePassword(password) || !users.VaLidateEmail(email))
                return false;

            return true;
        }
    }
}
