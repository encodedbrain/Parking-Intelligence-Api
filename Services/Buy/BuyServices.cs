using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services;

public class BuyServices : Tables
{
    private readonly User _user = new();

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

    public void MakingPurchase(BuySchema prop)
    {
        using (var db = new ParkingDb())
        {
            var gettingUserId = db.Users.FirstOrDefault(
                user => user.Password == _user.EncryptingPassword(prop.Password) && user.Email == prop.Email
            );
            if (gettingUserId is null) return;

            var findUserOfId = db.Users.Find(gettingUserId.Id);

            if (findUserOfId == null) return;

            var allUsers = db.Users.FirstOrDefault(user => user.UserData.UserId == findUserOfId.Id);

            if (allUsers is null)
                return;
            allUsers.Buys = new List<Buy>()
            {
                new()
                {
                    Date = DateTime.Now,
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
                        DateEntry = DateTime.Now,
                        DepartureDate = DateTime.Now,
                        StayTime = DateTime.Now,
                        TicketNumber = GenerateCredential(),
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
            allUsers.Vehicles = new List<Vehicle>()
            {
                new()
                {
                    Species = prop.Species,
                    Brand = prop.Brand,
                    Color = prop.Color,
                    Model = prop.Model,
                    Year = prop.Year,
                    LicensePlate = prop.LicensePlate,
                    Name = prop.Name
                }
            };
            db.Users.Update(allUsers);
            db.SaveChanges();
        }
    }

    private int GenerateCredential()
    {
        return new Random().Next();
    }

    private string FormatTime()
    {
        var date = DateTime.Now;
        return $"{date:hh:mm:ss}";
    }


    private decimal ChangeToRreceive(decimal purchaseExpense, decimal purchaseAmountPaid)
    {
        var change = purchaseAmountPaid - purchaseExpense;

        return change;
    }
}