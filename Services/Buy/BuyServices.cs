using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.Models;
using Parking_Intelligence_Api.Schemas;
using ZstdSharp.Unsafe;

namespace Parking_Intelligence_Api.Services;

public class BuyServices
{
    internal bool ValidateCredentials(BuySchema purchase)
    {
        using (var db = new ParkingDB())
        {
            var getIdUser = db.Users.FirstOrDefault(
                u => u.password == new User().EncryptingPassword(purchase.Password) && u.email == purchase.Email
            );
            if (getIdUser == null) return false;
        }

        return true;
    }
    public void MakingPurchase(BuySchema purchase)
    {
        using (var db = new ParkingDB())
        {
            var User = new User();

            var getIdUser = db.Users.FirstOrDefault(
                u => u.password == User.EncryptingPassword(purchase.Password) && u.email == purchase.Email
            );

            if (getIdUser == null) return;

            var findUserOfId = db.Users.Find(getIdUser.id);

            if (findUserOfId == null) return;

            var users = db.Users.Where(u => u.userData.userId == findUserOfId.id).FirstOrDefault();

            if (users == null)
                return;
            users.Buys = new List<Buy>()
            {
                new()
                {
                    date = DateTime.Now,
                    vacancyType = formatVacancyType(purchase.VacancyType),
                    value = purchase.Value,
                    paymentMethod = new PaymentMethod()
                    {
                        method = purchase.Method
                    },
                    invoice = new Invoice()
                    {
                        expense = purchase.Value,
                        amountPaid = purchase.AmountPaid,
                        Change = changeToRreceive(purchase.Value, purchase.AmountPaid),
                        dateEntry = DateTime.Now,
                        departureDate = DateTime.Now,
                        stayTime = DateTime.Now,
                        ticketNumber = generateCredential(),
                        Ticket = new Ticket()
                        {
                            date = DateTime.Now,
                            ticketNumber = generateCredential(),
                            sequence = generateCredential(),
                            hour = formatTime()
                        }
                    }
                }
            };
            db.Users.Update(users);
            db.SaveChanges();
        }
    }

    private int generateCredential()
    {
        return new Random().Next();
    }

    private string formatTime()
    {
        var date = DateTime.Now;
        return $"{date:hh:mm:ss}";
    }

    private string formatVacancyType(string type)
    {
        var rotary = "rotary";
        var monthlyPayer = "monthly payer";

        if (string.IsNullOrEmpty(type)) return string.Empty;
        if (type == "rotativo" || type == "rotary") return rotary;
        ;
        if (type == "mensalista" || type == "monthly payer") return monthlyPayer;

        return string.Empty;
    }

    private decimal changeToRreceive(decimal purchaseExpense, decimal purchaseAmountPaid)
    {
        var change = purchaseAmountPaid - purchaseExpense;

        return change;
    }
}