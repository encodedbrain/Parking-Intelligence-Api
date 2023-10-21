using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas.User;

namespace Parking_Intelligence_Api.Services.User;

public class CreateUserServices
{
    private Models.User _user = new();

    internal bool SearchingforUser(string email, string cpf, string phone)
    {
        using (var db = new ParkingDb())
        {
            if (
                string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(cpf)
                || string.IsNullOrEmpty(phone)
            )
                return false;
            var searchingforUser = db.Users.Any(
                user =>
                    user.Email == email
                    || user.UserData.Cpf == cpf
                    || user.UserData.Phone == phone
            );

            return searchingforUser;
        }
    }

    internal bool ValidateCredentials(
        string email,
        string nickname,
        string fullname,
        string cpf,
        string phone,
        string password
    )
    {
        if (SearchingforUser(email, cpf, phone))
            return false;
        else if (!_user.VaLidateEmail(email))
            return false;
        else if (!_user.ValidateName(nickname))
            return false;
        else if (!_user.ValidateName(fullname))
            return false;
        else if (!_user.ValidateCpf(cpf))
            return false;
        else if (!_user.ValidatePhone(phone))
            return false;
        else if (!_user.ValidatePassword(password))
            return false;

        return true;
    }

    internal async Task<object> CreateNewUser(UserSchema prop)
    {
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
}