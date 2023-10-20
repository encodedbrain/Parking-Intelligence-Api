using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Data;


namespace Parking_Intelligence_Api.Services.ParkedTime;

public class ParkedTimeService
{
    public async Task<bool> CalculatesParkedTime(string email, string password)
    {
        var user = new Models.User();

        using (var db = new ParkingDb())
        {
            var vacancys = db.Buys.Include(buy => buy.Invoice).FirstOrDefault(buy => buy.VacancyType == "rotativo"
                && buy.User.Id == buy.UserId && buy.User.Password ==
                user.EncryptingPassword(password)
                && buy.User.Email == email);

            if (vacancys is null) return false;


            var vehicle = db.Vehicles.FirstOrDefault(vehicle =>
                vehicle.Status == "estacionado" && vehicle.UserId == vacancys.User.Id &&
                vehicle.LicensePlate == vacancys.VehicleIdentifier);

            if (vehicle is null) return false;

            if (vehicle.Status == "retirado")
            {
                var culture = new CultureInfo("en-US");

                var limit = vacancys.Invoice.LimitTime.Replace(":", "."
                );
                var exit = vacancys.Invoice.StayTime.Replace(":", "."
                );

                var convertLimit = Convert.ToDecimal(limit, culture);
                var convertExit = Convert.ToDecimal(exit, culture);

                if (convertExit < convertLimit)
                {
                    var total = CalculatesParkedTime(vacancys.Value);
                    vacancys.Value = total;
                }
            }


            if (DateTime.Now.ToString("t") == vacancys.Invoice.LimitTime)
            {
                vehicle.Status = "retirado";
                vacancys.Invoice.StayTime = DateTime.Now.ToString("t");
            }

            db.Vehicles.Update(vehicle);
            db.Buys.Update(vacancys);
            await db.SaveChangesAsync();
        }

        return true;
    }

    private decimal CalculatesParkedTime(decimal vacancysValue)
    {
        var fees = vacancysValue * 15 / 100;
        return fees + vacancysValue;
    }
}