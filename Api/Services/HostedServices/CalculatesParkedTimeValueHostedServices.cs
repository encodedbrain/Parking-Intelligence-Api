// using Parking_Intelligence_Api.Data;
// using Parking_Intelligence_Api.Models;
// using Parking_Intelligence_Api.Schemas;
// using Timer = System.Timers.Timer;
// using System.Globalization;
// using Microsoft.EntityFrameworkCore;
//
// namespace Parking_Intelligence_Api.Services.HostedServices;
//
// public class CalculatesParkedTimeValueHostedServices : BackgroundService
// {
//     private readonly IServiceProvider _serviceProvider;
//     private readonly ILogger<CalculatesParkedTimeValueHostedServices> _logger;
//
//     public CalculatesParkedTimeValueHostedServices(ILogger<CalculatesParkedTimeValueHostedServices> logger,
//         IServiceProvider serviceProvider)
//     {
//         _logger = logger;
//         _serviceProvider = serviceProvider;
//     }
//
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         while (!stoppingToken.IsCancellationRequested)
//             try
//             {
//                 CalculatesParkedTime(new LoginSchema());
//                 await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);   
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//                 throw;
//             }
//     }
//
//     public async void CalculatesParkedTime(LoginSchema prop)
//     {
//         User user = new User();
//         
//         using (var db = new ParkingDb())
//         {
//             var vacancys = db.Buys.Include(buy => buy.Invoice).FirstOrDefault(buy => buy.VacancyType == "rotativo"
//                                                          && buy.User.Id == buy.UserId && buy.User.Password ==
//                                                          user.EncryptingPassword(prop.Password)
//                                                          && buy.User.Email == prop.Email);
//
//             var vehicle = db.Vehicles.FirstOrDefault(vehicle =>
//                 vacancys != null && vehicle.Status == "estacionado" && vehicle.UserId == vacancys.User.Id &&
//                 vehicle.LicensePlate == vacancys.VehicleIdentifier);
//             
//             if (vacancys is null || vehicle is null) return;
//
//             if (vehicle.Status == "retirado")
//             {
//                 var culture = new CultureInfo("en-US");
//
//                 var limit = vacancys.Invoice.LimitTime.Replace(":", "."
//                 );
//                 var exit = vacancys.Invoice.StayTime.Replace(":", "."
//                 );
//
//                 var convertLimit = Convert.ToDecimal(limit, culture);
//                 var convertExit = Convert.ToDecimal(exit, culture);
//
//                 if (convertExit < convertLimit)
//                 {
//                     var total = CalculatesParkedTime(vacancys.Value);
//                     vacancys.Value = total;
//                 }
//             }
//
//
//             if (DateTime.Now.ToString("t") == vacancys.Invoice.LimitTime)
//             {
//                 vehicle.Status = "retirado";
//                 vacancys.Invoice.StayTime = DateTime.Now.ToString("t");
//             }
//
//             db.Vehicles.Update(vehicle);
//             db.Buys.Update(vacancys);
//             await db.SaveChangesAsync();
//         }
//     }
//
//     private decimal CalculatesParkedTime(decimal vacancysValue)
//     {
//         var fees = vacancysValue * 15 / 100;
//         return fees + vacancysValue;
//     }
// }