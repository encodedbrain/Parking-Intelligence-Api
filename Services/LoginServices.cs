using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services
{
    public class LoginServices
    {
        ParkingDB DB = new ParkingDB();
        User users = new User();

        internal bool FindUser(Login user)
        {
            var filter = DB.Users
                .Where(
                    u =>
                        u.password == users.EncryptingPassword(user.password)
                        && u.email == user.email
                )
                .Select(user => user)
                .First();

            if (filter == null)
                return false;
            ReturnUser(user);
            return true;
        }

        internal object ReturnUser(Login user)
        {
            var filter = DB.Users
                .Where(
                    u =>
                        u.password == users.EncryptingPassword(user.password)
                        && u.email == user.email
                )
                .Select(user => user)
                .First();
            if (filter != null)
            {
                var newUser = new User { email = user.email, password = user.password };
                object token = ParkingServices.GenerateToken(filter) ?? newUser;

                filter.password = string.Empty;
                return new { filter, token };
            }
            return new { };
        }

        internal bool ValidateCredentials(string email, string password, Login user)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;
            if (!users.ValidatePassword(password) || !users.VaLidateEmail(email))
                return false;

            return true;
        }
    }
}
