using Parking_Intelligence_Api.Data;

namespace Parking_Intelligence_Api.Services.User;

public class UpdateUserServices
{
    readonly Models.User _user = new Models.User();

    public bool UpdatePassword(string? value, Models.User prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.Find(prop.Id);

            if (gettingUserId is null)
                return false;

            var allUsers = db.Users.FirstOrDefault(user => user.UserData.UserId == gettingUserId.Id);

            if (allUsers is null)
                return false;

            allUsers.Password = _user.EncryptingPassword(value);

            db.Users.Update(allUsers);
            db.SaveChangesAsync();
            return true;
        }
    }

    public bool UpdateEmail(string? value, Models.User prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.Find(prop.Id);
            if (gettingUserId is null) return false;
            
            var allUsers = db.Users.FirstOrDefault(user => user.UserData.UserId == gettingUserId.Id);

            if (allUsers is null || value is null)
                return false;

            allUsers.Email = value;

            db.Users.Update(allUsers);
            db.SaveChangesAsync();
            return true;
        }
    }

    public bool UpdateNickname(string? value, Models.User prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.Find(prop.Id);
            
            if (gettingUserId is null) return false;
            
            var allUsers = db.Users.FirstOrDefault(user => user.UserData.UserId == gettingUserId.Id);

            if (allUsers is null || value is null)
                return false;

            allUsers.Nickname = value;

            db.Users.Update(allUsers);
            db.SaveChangesAsync();
            return true;
        }
    }

    public bool UpdateAddress(string? value, Models.User prop)
    {

        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.Find(prop.Id);
            
            if (gettingUserId is null) return false;
            
            var allUsers = db.UserDatas.FirstOrDefault(user => user.UserId == gettingUserId.Id);

            if (allUsers is null || value is null)
                return false;

            allUsers.Address = value;

            db.UserDatas.Update(allUsers);
            db.SaveChangesAsync();
            return true;
        }
    }

    public bool UpdatePhone(string? value, Models.User prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.Find(prop.Id);

            if (gettingUserId is null) return false;
            
            var allUsers = db.UserDatas.FirstOrDefault(user => user.UserId == gettingUserId.Id);

            if (allUsers is null ||  value is null || !_user.ValidatePhone(value))
                return false;

            allUsers.Phone = value;

            db.UserDatas.Update(allUsers);
            db.SaveChangesAsync();
            return true;
        }
    }

    public bool UpdateFullname(string? value, Models.User prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.Find(prop.Id);
            
            if (gettingUserId is null) return false;
            
            var allUsers = db.UserDatas.FirstOrDefault(user => user.UserId == gettingUserId.Id);

            if (allUsers is null ||  value is null || !_user.ValidateName(value))
                return false;

            allUsers.FullName = value;

            db.UserDatas.Update(allUsers);
            db.SaveChangesAsync();
            return true;
        }
    }

    internal bool VerifyCredentials(
        string? fieldEdit,
        string? password,
        string? email,
        string? value
    )
    {
        if (
            string.IsNullOrEmpty(fieldEdit)
            || string.IsNullOrEmpty(password)
            || string.IsNullOrEmpty(email)
            || string.IsNullOrEmpty(value)
            || !_user.ValidatePassword(password)
            || !_user.VaLidateEmail(email)
        )
            return false;
        using (var db = new ParkingDb())
        {
            var gettingUser = db.Users.FirstOrDefault(
                user => user.Password == _user.EncryptingPassword(password) && user.Email == email
            );

            if (gettingUser is null) return false;

            switch (fieldEdit)
            {
                case "password":
                    UpdatePassword(value, gettingUser);
                    break;
                case "email":
                    UpdateEmail(value, gettingUser);
                    break;
                case "nickname":
                    UpdateNickname(value, gettingUser);
                    break;
                case "address":
                    UpdateAddress(value, gettingUser);
                    break;
                case "phone":
                    UpdatePhone(value, gettingUser);
                    break;
                case "fullname":
                    UpdateFullname(value, gettingUser);
                    break;
            }

            

            return true;
        }
    }
}