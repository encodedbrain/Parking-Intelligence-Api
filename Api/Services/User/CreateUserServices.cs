using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas.User;

namespace Parking_Intelligence_Api.Services.User;

public class CreateUserServices
{
    private Models.User _user = new();


    internal async Task<object> CreateNewUser(UserSchema prop)
    {
        if (!ValidateCredentials(prop)) return false;
        if (!SearchingforUser(prop)) return false;

        using (var db = new ParkingDb())
        {
            var user = new Models.User
            {
                Nickname = prop.Nickname,
                Password = _user.EncryptingPassword(prop.Password),
                Email = prop.Email,
                UserData = new UserData
                {
                    Address = prop.Address,
                    Cpf = _user.ReturnCpfFormated(prop.Cpf),
                    FullName = prop.Fullname,
                    Phone = prop.Phone
                }
            };
            await db.AddAsync(user);
            await db.SaveChangesAsync();

            var token = TokenServices.GenerateToken(user);

            user.Password = string.Empty;
            user.UserData.Cpf = string.Empty;
            user.UserData = null!;
            return new
            {
                user.Email,
                user.Nickname, token
            };
        }
    }

    internal bool SearchingforUser(UserSchema prop)
    {
        using (var db = new ParkingDb())
        {
            if (
                string.IsNullOrEmpty(prop.Email)
                || string.IsNullOrEmpty(prop.Cpf)
                || string.IsNullOrEmpty(prop.Phone)
            )
                return false;
            var searchingforUser = db.Users.Any(
                user =>
                    user.Email == prop.Email
                    || user.UserData.Cpf == prop.Cpf
                    || user.UserData.Phone == prop.Phone
                    || user.Nickname == prop.Nickname
            );

            return searchingforUser;
        }
    }

    internal bool ValidateCredentials(
        UserSchema prop
    )
    {
        if (!_user.VaLidateEmail(prop.Email))
            return false;
        else if (!_user.ValidateName(prop.Nickname))
            return false;
        else if (!_user.ValidateName(prop.Fullname))
            return false;
        else if (!_user.ValidateCpf(prop.Cpf))
            return false;
        else if (!_user.ValidatePhone(prop.Phone))
            return false;
        else if (!_user.ValidatePassword(prop.Password))
            return false;


        return true;
    }
}