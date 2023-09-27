using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services
{
    public class CreateUserServices
    {
        ParkingDB DB = new ParkingDB();
        User userInstantiatesEntity = new User();

        internal bool searchingforUser(string email, string cpf, string phone)
        {
            if (
                string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(cpf)
                || string.IsNullOrEmpty(phone)
            )
                return false;
            var searchingforUser = DB.Users.Any(
                column =>
                    column.email == email
                    || column.UserData.Cpf == cpf
                    || column.UserData.phone == phone
            );

            return searchingforUser;
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
            if (searchingforUser(email, cpf, phone))
                return false;
            else if (!userInstantiatesEntity.VaLidateEmail(email))
                return false;
            else if (!userInstantiatesEntity.ValidateName(nickname))
                return false;
            else if (!userInstantiatesEntity.ValidateName(fullname))
                return false;
            else if (!userInstantiatesEntity.ValidateCpf(cpf))
                return false;
            else if (!userInstantiatesEntity.ValidatePhone(phone))
                return false;
            else if (!userInstantiatesEntity.ValidatePassword(password))
                return false;

            return true;
        }

        internal async Task<object> CreateNewUser(UserSchema user)
        {
            using (var context = new ParkingDB())
            {
                Random rnd = new Random();
                var User = new User
                {
                    nickname = user.nickname,
                    password = userInstantiatesEntity.EncryptingPassword(user.password),
                    email = user.email,
                    UserData = new UserData
                    {
                        address = user.address,
                        Cpf = userInstantiatesEntity.ReturnCpfFormated(user.cpf),
                        fullName = user.Fullname,
                        phone = user.phone
                    }
                };
                await context.AddAsync(User);
                await context.SaveChangesAsync();

                var generateToken = ParkingServices.GenerateToken(User);

                User.password = "";
                User.UserData.Cpf = "";
                User.UserData.address = "";
                return new { User, generateToken };
            }
        }
    }
}
