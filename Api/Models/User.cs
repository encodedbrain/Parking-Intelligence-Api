using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Parking_Intelligence_Api.interfaces;

namespace Parking_Intelligence_Api.Models
{
    public class User : IUser
    {
        public User()
        {
        }

        public User(string email, string nickname, string password)
        {
            this.Email = email;
            this.Nickname = nickname;
            this.Password = password;
        }

        public int Id { get; set; }
        public string Email { get; internal set; } = null!;
        public string Nickname { get; internal set; } = null!;
        public string Password { get; internal set; } = null!;
        public virtual UserData UserData { get; internal set; } = null!;

        [NotMapped]
        public virtual ICollection<Vehicle> Vehicles { get; internal set; } = null!;

        public virtual ICollection<Buy> Buys { get; internal set; } = null!;

        public bool ValidatePassword(string password)
        {
            Regex regex = new Regex(
                "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"
            );

            if (string.IsNullOrEmpty(password) || !regex.IsMatch(password))
            {
                return false;
            }
            else
            {
                return true;
            }
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
            else
            {
                Match checkingFormat = rgx.Match(cpf);

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
                else
                {
                    return false;
                }
            }
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

            string cpfFormated = Regex.Replace(cpf, "[^0-9a-zA-Z]+", "");

            return cpfFormated;
        }

        public bool ValidatePhone(string? phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            Regex regex = new Regex("^\\+?[1-9][0-9]{7,14}$");

            if (regex.IsMatch(phone))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
