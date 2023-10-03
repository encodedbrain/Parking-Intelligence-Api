using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;

namespace Parking_Intelligence_Api.Services;

public class BuyServices
{
    public async Task<bool> MakingPurchase(BuySchema purchase)
    {
        using (var db = new ParkingDB())
        {
            var getIdUserc = db.Users.FirstOrDefault(
                user =>
                    user.Email == purchase.Email
                    && user.Password == new User().EncryptingPassword(purchase.Password)
            );

            if (getIdUserc == null)
                return false;

            var findUser = db.Users.Find(getIdUserc.Id);

            if (findUser == null)
                return false;

            var buy = new List<Buy>()
            {
                new Buy()
                {
                    Date = DateTime.Now,
                    Value = purchase.Value,
                    VacancyType = purchase.VacancyType,
                    PaymentMethod = new PaymentMethod() { Method = purchase.Method },
                    Invoice = new Invoice()
                    {
                        Expense = purchase.Expense,
                        AmountPaid = purchase.AmountPaid,
                        Change = changeToRreceive(purchase.Expense, purchase.AmountPaid),
                        DateEntry = DateTime.Now,
                        DepartureDate = DateTime.Now,
                        TicketNumber = new Random().Next(),
                        StayTime = DateTime.Now,
                        Ticket = new Ticket()
                        {
                            Date = DateTime.Now,
                            TicketNumber = new Random().Next(),
                            Sequence = new Random().Next()
                        }
                    },
                    User = findUser
                }
            };

            await db.Buys.AddRangeAsync(buy);
            await db.SaveChangesAsync();
        }

        return true;
    }

    private decimal changeToRreceive(decimal purchaseExpense, decimal purchaseAmountPaid)
    {
        decimal change = purchaseAmountPaid - purchaseExpense;

        return change;
    }
}
