using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Services
{
    public class UpdateUserServices
    {
        ParkingDB DB = new ParkingDB();
        User User = new User();
        UserData UserData = new UserData();

        public bool UpdatePassword(string? value, User getUser)
        {
            if (getUser == null)
                return false;

            using (var DB = new ParkingDB())
            {
                var User = DB.Users.Find(getUser.id);

                if (User == null)
                    return false;

                var users = DB.Users.Where(u => u.UserData.user_id == User.id).FirstOrDefault();

                if (users == null)
                    return false;

                users.password = User.EncryptingPassword(value);

                DB.Users.Update(users);
                DB.SaveChangesAsync();
                return true;
            }
        }

        public bool UpdateEmail(string? value, User getUser)
        {
            if (getUser == null)
                return false;
            using (var DB = new ParkingDB())
            {
                var User = DB.Users.Find(getUser.id);

                if (User == null)
                    return false;

                var users = DB.Users.Where(u => u.UserData.user_id == User.id).FirstOrDefault();

                if (users == null)
                    return false;

                users.email = value;

                DB.Users.Update(users);
                DB.SaveChangesAsync();
                return true;
            }
        }

        public bool UpdateNickname(string? value, User getUser)
        {
            if (getUser == null)
                return false;

            using (var DB = new ParkingDB())
            {
                var User = DB.Users.Find(getUser.id);

                if (User == null)
                    return false;

                var users = DB.Users.Where(x => x.UserData.user_id == User.id).FirstOrDefault();

                if (users == null)
                    return false;

                users.nickname = value;

                DB.Users.Update(users);
                DB.SaveChangesAsync();
                return true;
            }
        }

        public bool UpdateAddress(string? value, User getUser)
        {
            if (getUser == null)
                return false;

            using (var DB = new ParkingDB())
            {
                var User = DB.Users.Find(getUser.id);

                if (User == null)
                    return false;

                var users = DB.UserDatas.Where(x => x.user_id == User.id).FirstOrDefault();

                if (users == null)
                    return false;

                users.address = value;

                DB.UserDatas.Update(users);
                DB.SaveChangesAsync();
                return true;
            }
        }

        public bool UpdatePhone(string? value, User getUser)
        {
            if (getUser == null || !User.ValidatePhone(value))
                return false;

            using (var DB = new ParkingDB())
            {
                var User = DB.Users.Find(getUser.id);

                if (User == null)
                    return false;

                var users = DB.UserDatas.Where(x => x.user_id == User.id).FirstOrDefault();

                if (users == null)
                    return false;

                users.phone = value;

                DB.UserDatas.Update(users);
                DB.SaveChangesAsync();
                return true;
            }
        }

        public bool UpdateFullname(string? value, User getUser)
        {
            if (getUser == null || !User.ValidateName(value))
                return false;

            using (var DB = new ParkingDB())
            {
                var User = DB.Users.Find(getUser.id);

                if (User == null)
                    return false;

                var userData = DB.UserDatas.Where(u => u.user_id == User.id).FirstOrDefault();

                if (userData == null)
                    return false;

                userData.fullName = value;

                DB.UserDatas.Update(userData);
                DB.SaveChangesAsync();
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
                || !User.ValidatePassword(password)
                || !User.VaLidateEmail(email)
            )
                return false;

            var getUser = DB.Users.FirstOrDefault(
                u => u.password == User.EncryptingPassword(password) && u.email == email
            );

            if(getUser == null) return false;

            switch (fieldEdit)
            {
                case "password":
                    UpdatePassword(value, getUser);
                    break;
                case "email":
                    UpdateEmail(value, getUser);
                    break;
                case "nickname":
                    UpdateNickname(value, getUser);
                    break;
                case "address":
                    UpdateAddress(value, getUser);
                    break;
                case "phone":
                    UpdatePhone(value, getUser);
                    break;
                case "fullname":
                    UpdateFullname(value, getUser);
                    break;
            }
            ;

            return true;
        }
    }
}