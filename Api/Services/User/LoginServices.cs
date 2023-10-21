using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas.User;

namespace Parking_Intelligence_Api.Services.User;

public class LoginServices
{
    private Models.User _user = new();

    internal object ReturnUser(LoginSchema prop
    )
    {
        using (var db = new ParkingDb())
        {
            var user = db.Users.SingleOrDefault(
                user => user.Email == prop.Email && user.Password == _user.EncryptingPassword(prop.Password)
            );

            if (user is null)
                return false;


            var token = TokenServices.GenerateToken(user);
            user.Password = string.Empty;

            return new { user.Id, user.Email, user.Nickname, token };
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