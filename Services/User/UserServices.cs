using Parking_Intelligence_Api.Schemas.User;

namespace Parking_Intelligence_Api.Services.User;

public class UserServices
{
    public Models.User Service = new Models.User();

    public bool ValidateCredentials(LoginSchema prop)
    {
        if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
            return false;
        if (!new Models.User().ValidatePassword(prop.Password) || !new Models.User().VaLidateEmail(prop.Email))
            return false;

        return true;
    }
}