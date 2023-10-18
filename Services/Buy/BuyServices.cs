using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;
using Parking_Intelligence_Api.Schemas.buy;

namespace Parking_Intelligence_Api.Services.Buy;

public class BuyServices : Tables
{
    private readonly Models.User _user = new();

    internal bool ValidateCredentials(BuySchema prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.FirstOrDefault(
                user => user.Password == _user.EncryptingPassword(prop.Password) && user.Email == prop.Email
            );
            if (gettingUserId is null) return false;
        }

        return true;
    }

    public bool MakingPurchase(BuySchema prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.FirstOrDefault(
                user => user.Password == _user.EncryptingPassword(prop.Password) && user.Email == prop.Email
            );
            if (gettingUserId is null) return false;

            var findUserOfId = db.Users.Find(gettingUserId.Id);

            if (findUserOfId == null) return false;

            var allUsers = db.Users.FirstOrDefault(user => user.UserData.UserId == findUserOfId.Id);

            if (allUsers is null)
                return false;
            allUsers.Buys = new List<Models.Buy>()
            {
                new()
                {
                    Date = DateTime.Now.ToString("d"),
                    VacancyType = prop.VacancyType,
                    Value = informsTheValueOfTheVacancy(prop.Species),
                    VehicleIdentifier = prop.LicensePlate,
                    PaymentMethod = new PaymentMethod()
                    {
                        Method = prop.Method
                    },
                    Invoice = new Invoice()
                    {
                        Expense = prop.Value,
                        AmountPaid = prop.AmountPaid,
                        Change = ChangeToRreceive(prop.Value, prop.AmountPaid),
                        DateEntry = FormatTime(),
                        DepartureDate = DateTime.Now,
                        StayTime = "",
                        TicketNumber = GenerateCredential(),
                        LimitTime = VacancyTypeCheck(prop.VacancyType),
                        Ticket = new Ticket()
                        {
                            Date = DateTime.Now,
                            TicketNumber = GenerateCredential(),
                            Sequence = GenerateCredential(),
                            Hour = FormatTime()
                        }
                    }
                }
            };
            allUsers.Vehicles = new List<Models.Vehicle>()
            {
                new()
                {
                    Species = prop.Species,
                    Brand = prop.Brand,
                    Color = prop.Color,
                    Model = prop.Model,
                    Year = prop.Year,
                    LicensePlate = prop.LicensePlate,
                    Status = "estacionado",
                    Name = prop.Name
                }
            };
            db.Users.Update(allUsers);
            db.SaveChanges();
        }

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

        if (vacancy == "rotativo")
        {
            var add = hour.AddHours(2).ToString("t");
            return add;
        }


        return hour.AddMonths(1).ToString("d");
    }
}