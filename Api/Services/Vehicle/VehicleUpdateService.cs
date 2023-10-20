using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Schemas.vehicle;

namespace Parking_Intelligence_Api.Services.Vehicle;

public class VehicleUpdateService
{
    public async Task<bool> VehicleUpdate(UpdateVehicleSchema prop, string vacancy)
    {
        var user1 = new Models.User();
        var rotary = "rotary";
        var monthlyPayer = "monthly payer";
        var parked = "parked";
        var withdrawn = "withdrawn";

        if (!user1.ValidatePassword(prop.Password)) return false;
        if (!user1.VaLidateEmail(prop.Email)) return false;

        using (var db = new ParkingDb())
        {
            var user = db.Users.FirstOrDefault(
                user => user.Email == prop.Email
                        && user.Password == user1.EncryptingPassword(prop.Password)
            );
            if (user is null) return false;


            if (vacancy == rotary)
            {
                var buy = db.Buys.Include(buy => buy.Invoice).FirstOrDefault(buy => buy.VacancyType == rotary
                    && buy.User.Id == buy.UserId && buy.User.Password ==
                    user.EncryptingPassword(prop.Password)
                    && buy.User.Email == prop.Email);

                if (buy is null) return false;

                var vehicle = db.Vehicles.FirstOrDefault(vehicle =>
                    vehicle.Status == parked && vehicle.UserId == buy.UserId &&
                    vehicle.LicensePlate == prop.VehicleIdentifier);

                if (vehicle is null) return false;

                vehicle.Status = withdrawn;
                buy.Invoice.StayTime = DateTime.Now.ToString("t");

                var culture = new CultureInfo("en-US");

                var limit = buy.Invoice.LimitTime.Replace(":", "."
                );
                var exit = buy.Invoice.StayTime.Replace(":", "."
                );

                var convertLimit = Convert.ToDecimal(limit, culture);
                var convertExit = Convert.ToDecimal(exit, culture);

                if (convertExit < convertLimit)
                {
                    var total = CalculatesParkedTime(buy.Value, rotary);
                    buy.Value = total;
                    buy.Invoice.Expense = total;
                }

                db.Vehicles.Update(vehicle);
                db.Buys.Update(buy);
            }

            if (vacancy == monthlyPayer)
            {
                var buy = db.Buys.Include(buy => buy.Invoice).FirstOrDefault(buy => buy.VacancyType == monthlyPayer
                    && buy.User.Id == buy.UserId && buy.User.Password ==
                    user.EncryptingPassword(prop.Password)
                    && buy.User.Email == prop.Email);

                if (buy is null) return false;

                var vehicle = db.Vehicles.FirstOrDefault(vehicle =>
                    vehicle.Status == parked && vehicle.UserId == buy.User.Id &&
                    vehicle.LicensePlate == buy.VehicleIdentifier);

                if (vehicle is null) return false;


                vehicle.Status = withdrawn;
                buy.Invoice.StayTime = DateTime.Now.ToString("d");

                var culture = new CultureInfo("en-US");

                var limit = buy.Invoice.LimitTime[..^8];
                var exit = buy.Invoice.StayTime[..^8];

                var convertLimit = Convert.ToDecimal(limit, culture);
                var convertExit = Convert.ToDecimal(exit, culture);

                if (convertExit < convertLimit)
                {
                    var total = CalculatesParkedTime(buy.Value, monthlyPayer);
                    buy.Value = total;
                    buy.Invoice.Expense = total;
                }


                db.Vehicles.Update(vehicle);
                db.Buys.Update(buy);
            }


            await db.SaveChangesAsync();
        }


        return true;
    }

    private decimal CalculatesParkedTime(decimal vacancysValue, string vacancy)
    {
        decimal fees;
        var rotary = "rotary";

        if (vacancy == rotary)
        {
            fees = vacancysValue * 15 / 100;
            return fees + vacancysValue;
        }


        fees = vacancysValue * 10 / 100;
        return fees + vacancysValue;
    }
}