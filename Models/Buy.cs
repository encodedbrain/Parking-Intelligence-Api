using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.interfaces;
using Parking_Intelligence_Api.Schemas.buy;
using Parking_Intelligence_Api.Schemas.User;

namespace Parking_Intelligence_Api.Models
{
    public sealed class Buy : IBuy
    {
        public Buy(decimal value, string date, string vacancyType, Invoice invoice, PaymentMethod paymentMethod)
        {
            Value = value;
            Date = date;
            VacancyType = vacancyType;
            Invoice = invoice;
            PaymentMethod = paymentMethod;
        }

        public Buy()
        {
        }

        public Buy(string vehicleIdentifier)
        {
            VehicleIdentifier = vehicleIdentifier;
        }

        public int Id { get; private set; }
        public decimal Value { get; internal set; }

        public string VehicleIdentifier { get; private set; } = null!;
        public string Date { get; private set; } = null!;
        public string VacancyType { get; private set; } = null!;
        public Invoice Invoice { get; private set; } = null!;
        public PaymentMethod PaymentMethod { get; private set; } = null!;

        public Ticket Ticket { get; private set; } = null!;
        public int UserId { get; private set; }
        public User User { get; internal set; } = null!;


        public decimal InformsTheValueOfTheVacancy(string type)
        {
            string passenger = "passenger";
            string mixed = "mixed";


            var table = new[]
            {
                20.00,
                40.00,
                70.00
            };

            if (type == passenger) return new decimal(table[0]);
            if (type == mixed) return new decimal(table[1]);
            else return new decimal(table[2]);
        }

        public bool Purchase(BuySchema prop)
        {
            using var db = new ParkingDb();

            var user = db.Users.AsEnumerable().FirstOrDefault(p =>
                p.Password == p.EncryptingPassword(prop.Password) && p.Email == prop.Email);

            if (user is null) return false;

            user.Buys = new List<Buy>
            {
                new()
                {
                    Date = DateTime.Now.ToString("d"),
                    VacancyType = prop.VacancyType,
                    Value = this.InformsTheValueOfTheVacancy(prop.Species),
                    VehicleIdentifier = prop.LicensePlate,
                    Ticket = new Ticket()
                    {
                        Date = DateTime.Now.ToString("d"),
                        TicketNumber = new Random().Next(),
                        Sequence = new Random().Next(),
                        Hour = DateTime.Now.ToString("t")
                    },
                    Invoice = new Invoice()
                    {
                        Expense = this.InformsTheValueOfTheVacancy(prop.Species),
                        AmountPaid = prop.AmountPaid,
                        Change = ChangeToRreceive(this.InformsTheValueOfTheVacancy(prop.Species), prop.AmountPaid),
                        DateEntry = FormatTime(),
                        DepartureDate = DateTime.Now,
                        StayTime = string.Empty,
                        TicketNumber = GenerateCredential(),
                        LimitTime = VacancyTypeCheck(prop.VacancyType),
                    },
                    PaymentMethod = new PaymentMethod()
                    {
                        Method = string.Empty
                    }
                }
            };


            user.Vehicles = new List<Vehicle>()
            {
                new()
                {
                    Species = prop.Species,
                    Brand = prop.Brand,
                    Color = prop.Color,
                    Model = prop.Model,
                    Year = prop.Year,
                    LicensePlate = prop.LicensePlate,
                    Status = "parked",
                    Name = prop.Name
                }
            };


            db.Users.Update(user);
            db.SaveChanges();

            return true;
        }

        public object[]? GetPurchases(GetBuySchema prop)
        {
            using var db = new ParkingDb();
            var user = db.Users
                .Where(user =>
                    user.Nickname == prop.Name).Select(
                    user => new
                    {
                        User = user,
                        Buy = user.Buys
                    }).FirstOrDefault();


            if (user is null) return null;

            foreach (var buy in user.Buy)
            {
                buy.User = null!;
                return new object[] { buy };
            }

            return null;
        }

        public bool DeleteBuy(UserDeleteSchema prop)
        {

            using var db = new ParkingDb();
            var user = db.Users.AsEnumerable().FirstOrDefault(user => user.Email == prop.Email
                                                       && user.Password == user.EncryptingPassword(prop.Password));

            if (user is null) return false;

            var buy = db.Buys.Where(buy => buy.UserId == user.Id);
            var vehicle = db.Vehicles.Where(vehicle => vehicle.UserId == user.Id);

            foreach (var value in buy) db.Buys.Remove(value);

            foreach (var value in vehicle) db.Vehicles.Remove(value);

            db.SaveChanges();

            return true;
        }


        private int GenerateCredential()
        {
            return new Random().Next();
        }

        private string FormatTime()
        {
            var date = DateTime.Now;
            return date.ToString("t");
        }


        private decimal ChangeToRreceive(decimal purchaseExpense, decimal purchaseAmountPaid)
        {
            var change = purchaseAmountPaid - purchaseExpense;

            return change;
        }

        private string VacancyTypeCheck(string vacancy)
        {
            var hour = DateTime.Now;
            var rotary = "rotary";

            if (vacancy == rotary)
            {
                var add = hour.AddHours(2).ToString("t");
                return add;
            }


            return hour.AddMonths(1).ToString("d");
        }
    }
}