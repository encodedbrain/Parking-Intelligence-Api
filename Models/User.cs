using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.interfaces;
using Parking_Intelligence_Api.Schemas.User;
using Parking_Intelligence_Api.Services;

namespace Parking_Intelligence_Api.Models
{
    public sealed class User : IUser
    {
        public User(int id, string email, string nickname, string password, IFormFile image, UserData userData,
            ICollection<Vehicle> vehicles, ICollection<Buy> buys)
        {
            Id = id;
            Email = email;
            Nickname = nickname;
            Password = password;
            Image = image;
            UserData = userData;
            Vehicles = vehicles;
            Buys = buys;
        }


        public User()
        {
        }

        public int Id { get; private set; }
        public string Email { get; private set; } = null!;
        public string Nickname { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public string Photo { get; set; } = null!;
        [NotMapped] public IFormFile Image { get; set; } = null!;
        public UserData UserData { get; private set; } = null!;
        public ICollection<Vehicle> Vehicles { get; internal set; } = null!;
        public ICollection<Buy> Buys { get; internal set; } = null!;

        public bool ValidatePassword(string password)
        {
            Regex regex = new Regex(
                "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"
            );

            if (string.IsNullOrEmpty(password) || !regex.IsMatch(password))
            {
                return false;
            }

            return true;
        }

        public string EncryptingPassword(string? password)
        {
            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            if (password is null) return string.Empty;
            var arrayBytes = encoding.GetBytes(password);

            arrayBytes = hash.ComputeHash(arrayBytes);

            var strHex = new StringBuilder();

            foreach (var value in arrayBytes)
            {
                strHex.Append(value.ToString("x2"));
            }

            return strHex.ToString();
        }

        public bool ValidateCpf(string? cpf)
        {
            string pattern = "^[0-9]{3}.?[0-9]{3}.?[0-9]{3}-?[0-9]{2}";

            Regex rgx = new Regex(pattern);

            List<int> cpfDigits = new List<int>();
            List<int> nineDigitMultiplication = new List<int>();
            List<int> tenDigitMultiplication = new List<int>();

            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            var checkingFormat = rgx.Match(cpf);

            if (checkingFormat.Success)
            {
                string cpfFormated = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

                char[] arrayChars = cpfFormated.ToCharArray();

                for (int i = 0; i < 9; i++)
                {
                    var characters = (int)Char.GetNumericValue(arrayChars[i]);
                    cpfDigits.Add(characters);
                }

                for (int i = 0; i < cpfDigits.Count; i++)
                {
                    var mult = cpfDigits[i] * (10 - i);

                    nineDigitMultiplication.Add(mult);
                }

                var calculateSumOfNine = nineDigitMultiplication.Aggregate(SumOfNineDigits);

                int SumOfNineDigits(int ac, int c)
                {
                    return ac + c;
                }

                var nineDigit = (calculateSumOfNine * 10) % 11;

                if (nineDigit > 9)
                {
                    nineDigit = 0;
                    if (Char.GetNumericValue(cpfFormated[9]) == nineDigit)
                    {
                        cpfDigits.Add(0);
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Char.GetNumericValue(cpfFormated[9]) == nineDigit)
                {
                    cpfDigits.Add(nineDigit);
                }
                else
                {
                    return false;
                }

                for (var i = 0; i < cpfDigits.Count; i++)
                {
                    tenDigitMultiplication.Add(cpfDigits[i] * (11 - i));
                }

                var calculateSumOfTen = tenDigitMultiplication.Aggregate(SumOfTenDigits);

                int SumOfTenDigits(int ac, int c)
                {
                    return ac + c;
                }

                var tenDigit = (calculateSumOfTen * 10) % 11;

                if (tenDigit > 9)
                {
                    tenDigit = 0;
                    if (Char.GetNumericValue(cpfFormated[10]) == tenDigit)
                    {
                        cpfDigits.Add(0);
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Char.GetNumericValue(cpfFormated[10]) == tenDigit)
                {
                    cpfDigits.Add(tenDigit);
                }
                else
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool VaLidateEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            string pattern = "^\\S+@\\S+\\.\\S+$";
            Regex rgx = new Regex(pattern);

            return rgx.IsMatch(email);
        }

        public bool VerifyCharaterRepeat(string name)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();

            foreach (var chr in name)
            {
                int count = 0;
                foreach (var ch in name)
                {
                    if (chr == ch)
                    {
                        count++;
                    }
                }

                if (count > 1)
                {
                    dict.TryAdd(chr, count);
                }
            }

            if (dict.Any())
            {
                foreach (var kvp in dict)
                {
                    if (kvp.Value > 2) return false;
                }
            }

            return true;
        }

        public bool ValidateName(string? name)
        {
            if (string.IsNullOrEmpty(name) || !VerifyCharaterRepeat(name))
            {
                return false;
            }

            if (name.Length > 30)
            {
                return false;
            }

            return true;
        }

        private static bool ValidateFullName(string? name)
        {
            if (name is null) return false;
            return name.Length < 20;
        }

        public string ReturnCpfFormated(string? cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return "";
            }

            if (!ValidateCpf(cpf))
            {
                return "";
            }

            var cpfFormated = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

            return cpfFormated;
        }

        public bool ValidatePhone(string? phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            Regex regex = new Regex("^\\+?[1-9][0-9]{7,14}$");

            return regex.IsMatch(phone);
        }

        private static bool SearchingforUser(UserSchema value)
        {
            using var db = new ParkingDb();
            if (
                string.IsNullOrEmpty(value.Email)
                || string.IsNullOrEmpty(value.Cpf)
                || string.IsNullOrEmpty(value.Phone)
            )
                return false;
            var searchingforUser = db.Users.Any(
                user =>
                    user.Email == value.Email
                    || user.UserData.Cpf == value.Cpf
                    || user.UserData.Phone == value.Phone
                    || user.Nickname == value.Nickname
            );

            return searchingforUser;
        }

        private bool ValidateCredentials(
            UserSchema value
        )
        {
            if (!VaLidateEmail(value.Email))
                return false;
            if (!ValidateName(value.Nickname))
                return false;
            if (!ValidateFullName(value.Fullname))
                return false;
            if (!ValidateCpf(value.Cpf))
                return false;
            return ValidatePhone(value.Phone) && ValidatePassword(value.Password);
        }

        private bool VerifyFields(UpdatePasswordSchema prop)
        {
            if (
                string.IsNullOrEmpty(prop.Password)
                || string.IsNullOrEmpty(prop.Email)
                || string.IsNullOrEmpty(prop.NewPassword)
                || !ValidatePassword(prop.NewPassword)
                || !ValidatePassword(prop.Password)
                || !VaLidateEmail(prop.Email)
            )
            {
                return false;
            }

            return true;
        }

        public object Login(LoginSchema prop)
        {
            using var db = new ParkingDb();
            var user = db.Users.SingleOrDefaultAsync(
                user => user.Email == prop.Email && user.Password == EncryptingPassword(prop.Password)
            );


            if (user.Result == null) return false;

            var token = TokenServices.GenerateToken(user.Result);
            user.Result.Password = string.Empty;


            return new { user.Id, user.Result.Email, user.Result.Nickname, token };
        }

        public async Task<object> Create(UserSchema prop)
        
        {
            if (!ValidateCredentials(prop)) return false;
            if (SearchingforUser(prop)) return false;

            
            await using var db = new ParkingDb();
            var user = new User
            {
                Nickname = prop.Nickname,
                Password = EncryptingPassword(prop.Password),
                Email = prop.Email,
                Photo = string.Empty,
                UserData = new UserData
                {
                    Address = prop.Address,
                    Cpf = ReturnCpfFormated(prop.Cpf),
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

        public async Task<bool> Delete(LoginSchema prop)
        {
            await using var db = new ParkingDb();
            if (string.IsNullOrEmpty(prop.Email) || string.IsNullOrEmpty(prop.Password))
                return false;

            var userVerify = db.Users.Any(
                user => user.Email == prop.Email && user.Password == EncryptingPassword(prop.Password)
            );

            if (!userVerify) return false;

            var foreingkeyUser = db.Users
                .Where(user => user.Password == EncryptingPassword(prop.Password) && user.Email == prop.Email)
                .Select(u => u.UserData.UserId)
                .ToArray();

            var idUser = db.Users.Where(user => user.Id == foreingkeyUser[0]).Select(user => user.Id).ToArray();

            var getUser = db.Users.Find(idUser[0]);

            if (!userVerify || getUser is null)
                return false;

            var dataUser = db.UserDatas.Where(user => user.UserId == idUser[0]);
            var buyUser = db.Buys.Where(buy => buy.UserId == idUser[0]);
            var vehicleUser = db.Vehicles.Where(vehicle => vehicle.UserId == idUser[0]);

            foreach (var data in dataUser) db.UserDatas.Remove(data);
            foreach (var buy in buyUser) db.Buys.Remove(buy);
            foreach (var vehicle in vehicleUser) db.Vehicles.Remove(vehicle);

            db.Users.Remove(getUser);
            await db.SaveChangesAsync();

            return true;
        }

        public bool UpdatePassword(UpdatePasswordSchema prop)
        {
            VerifyFields(prop);
            using var context = new ParkingDb();

            var user = context.Users.FirstOrDefault(user =>
                user.Email == prop.Email && user.Password == this.EncryptingPassword(prop.Password));


            if (user is null) return false;
            else
            {
                user.Password = this.EncryptingPassword(prop.NewPassword);
                context.Update(user);
                context.SaveChanges();

            }


            return true;
        }
        public byte[] DownloadPhoto(DownloadSchema prop)
        {
            using var context = new ParkingDb();

            var user = context.Users.FirstOrDefault(user =>
                user.Email == prop.Email && user.Password == this.EncryptingPassword(prop.Password));


            if (user != null)
            {
                var dataBytes = System.IO.File.ReadAllBytes(user.Photo);
                return dataBytes;
            }

            return new byte[] { };
        }
        

        public bool  UpdatePhotoProfile(UpdatePhotoProfileSchema prop)
        {
            using var context = new ParkingDb();

            var user = context.Users.FirstOrDefault(user => user.Email == prop.Email
                                                            && user.Password == this.EncryptingPassword(prop.Password));
            var filePath = Path.Combine("Storage", prop.Image.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            prop.Image.CopyTo(fileStream);

            if (user != null)

            {
                user.Photo = filePath;
                context.Update(user);
                context.SaveChanges();
            }
            return true;
        }
    }
}