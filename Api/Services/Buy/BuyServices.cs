using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas.buy;

namespace Parking_Intelligence_Api.Services.Buy;

public class BuyServices
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
        Models.Buy buy = new Models.Buy();      

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
                    Value = buy.InformsTheValueOfTheVacancy(prop.Species),
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
                        Expense = buy.InformsTheValueOfTheVacancy(prop.Species),
                        AmountPaid = prop.AmountPaid,
                        Change = ChangeToRreceive(buy.InformsTheValueOfTheVacancy(prop.Species), prop.AmountPaid),
                        DateEntry = FormatTime(),
                        DepartureDate = DateTime.Now,
                        StayTime = "",
                        TicketNumber = GenerateCredential(),
                        LimitTime = VacancyTypeCheck(prop.VacancyType),
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
                    Status = "parked",
                    Name = prop.Name
                }
            };


            db.Users.Update(allUsers);
            db.SaveChanges();
        }

        return true;
    }
     // private decimal CalculatesParkedTime(decimal vacancysValue, string vacancy)
     // {
     //     decimal fees;
     //     var rotary = "rotary";
     //
     //     if (vacancy == rotary)
     //     {
     //         fees = vacancysValue * 15 / 100;
     //         return fees + vacancysValue;
     //     }
     //
     //
     //     fees = vacancysValue * 10 / 100;
     //     return fees + vacancysValue;
     // }
     
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