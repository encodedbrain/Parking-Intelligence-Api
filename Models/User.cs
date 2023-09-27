using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Parking_Intelligence_Api.interfaces;

namespace Parking_Intelligence_Api.Models
{
    public class User : IUser
    {
        internal int account_id;

        public User() { }

        public User(string email, string nickname, string password, int account_id)
        {
            this.email = email;
            this.nickname = nickname;
            this.password = password;
        }

        public int id { get; set; }
        public string email { get; internal set; }
        public string nickname { get; internal set; }
        public string password { get; internal set; }
        public virtual UserData UserData { get; internal set; }
        public virtual ICollection<Vehicle> Vehicles { get; internal set; }
        public virtual ICollection<Buy> Buys { get; internal set; }

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

        public bool ValidateName(string? name)
        {
            if (string.IsNullOrEmpty(name))
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
