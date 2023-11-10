// using Microsoft.EntityFrameworkCore;
// using Parking_Intelligence_Api.Data;
// using Parking_Intelligence_Api.Schemas.Ticket;
// using Parking_Intelligence_Api.Schemas.User;
//
// namespace Parking_Intelligence_Api.Services.Ticket;
//
// public class TicketService
// {
//     public async Task<bool> GenerateTicket(TicketSchema prop)
//     {
//         Models.User user1 = new Models.User();
//         using (var db = new ParkingDb())
//         {
//             var user = db.Users.FirstOrDefault(user =>
//                 user.Email == prop.Email && user.Password == user1.EncryptingPassword(prop.Password));
//             if (user is null) return false;
//
//             var buy = db.Buys.Include(buy => buy.Invoice)
//                 .FirstOrDefault(buy => buy.UserId == user.Id);
//
//             if (buy is null) return false;
//
//             // buy.Invoice.Ticket = new Models.Ticket()
//             // {
//             //     Date = buy.Date,
//             //     Hour = DateTime.Now.ToString("t"),
//             //     Sequence = buy.Invoice.TicketNumber,
//             //     TicketNumber = buy.Invoice.TicketNumber
//             // };
//             db.Buys.Update(buy);
//             await db.SaveChangesAsync();
//         }
//
//         return true;
//     }
// }