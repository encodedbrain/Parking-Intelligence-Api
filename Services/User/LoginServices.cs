using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services.User
{
    public class LoginServices
    {
        private Models.User _user = new Models.User();
        internal object? ReturnUser(LoginSchema prop
        )
        {
            using (var db = new ParkingDb())
            {
                var gettingUser = db.Users.SingleOrDefault(
                    user => user.Email == prop.Email && user.Password == _user.EncryptingPassword(prop.Password)
                );

                if (gettingUser is null)
                    return null;


                var token = ParkingServices.GenerateToken(gettingUser);
                gettingUser.Password = string.Empty;

                return new { gettingUser, token };
            }
                
        }

        internal bool ValidateCredentials(LoginSchema prop)
        {
            if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
                return false;
            if (!new Models.User().ValidatePassword(prop.Password) || !new Models.User().VaLidateEmail(prop.Email))
                return false;

            return true;
        }
    }
}
