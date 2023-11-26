using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Data;
using Parking_Intelligence_Api.interfaces;
using Parking_Intelligence_Api.Schemas.vehicle;

namespace Parking_Intelligence_Api.Models;

public class Vehicle : IVehicle
{
    public Vehicle(int id, string? model, string? color, string? name, int year, string? brand, string? licensePlate,
        string? species, int userId,string? status, User? user)
    {
        Id = id;
        Model = model;
        Color = color;
        Name = name;
        Year = year;
        Brand = brand;
        LicensePlate = licensePlate;
        Species = species;
        Status = status;
        UserId = userId;
        User = user;
    }

    public Vehicle()
    {
    }

    public int Id { get; private set; }
    public string? Model { get; internal set; }
    public string? Color { get; internal set; }
    public string? Name { get; set; }
    public int Year { get; internal set; }
    public string? Brand { get; internal set; }
    public string? LicensePlate { get; internal set; }
    public string? Status { get; internal set; }
    public string? Species { get; internal set; }
    public int UserId { get; internal set; }
    public User? User { get; internal set; }

    
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

    public object[] GetVehicles(GetVehicleSchema prop)
    {
        using var db = new ParkingDb();
        var user = db.Users
            .Where(user =>
                user.Nickname == prop.Name)
            .Select(
                user => new
                {
                    User = user,
                    Vehicle = user.Vehicles
                }).FirstOrDefault();


        if (user is null) return new object[] { };


        foreach (var vehicle in user.Vehicle)
        {
            if (vehicle.User != null) vehicle.User = null;

            return new object[]
            {
                vehicle
            };
        }

        return new object[] { };
    }

    public bool UpdateVehicle(UpdateVehicleSchema prop, string vacancy)
    {
        var rotary = "rotary";
        var monthlyPayer = "monthly payer";
        var parked = "parked";
        var withdrawn = "withdrawn";

        using var db = new ParkingDb();
        var user = db.Users.AsEnumerable().FirstOrDefault(
            user => user.Email == prop.Email
                    && user.Password == user.EncryptingPassword(prop.Password)
        );
        if (user is null) return false;


        if (vacancy == rotary)
        {
            var buy = db.Buys.Include(buy => buy.Invoice).Include(buy => buy.PaymentMethod).FirstOrDefault(buy =>
                buy.VacancyType == rotary
                && buy.User.Id == buy.UserId && buy.User.Password ==
                user.EncryptingPassword(prop.Password)
                && buy.User.Email == prop.Email);

            if (buy is null) return false;

            var vehicle = db.Vehicles.FirstOrDefault(vehicle =>
                vehicle.Status == parked && vehicle.UserId == buy.UserId &&
                vehicle.LicensePlate == prop.VehicleIdentifier);

            if (vehicle is null) return false;

            buy.PaymentMethod.Method = prop.Method;
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
            var buy = db.Buys.Include(buy => buy.Invoice).Include(buy => buy.PaymentMethod).FirstOrDefault(buy =>
                buy.VacancyType == monthlyPayer
                && buy.User.Id == buy.UserId && buy.User.Password ==
                user.EncryptingPassword(prop.Password)
                && buy.User.Email == prop.Email);

            if (buy is null) return false;

            var vehicle = db.Vehicles.FirstOrDefault(vehicle =>
                vehicle.Status == parked && vehicle.UserId == buy.User.Id &&
                vehicle.LicensePlate == buy.VehicleIdentifier);

            if (vehicle is null) return false;


            buy.PaymentMethod.Method = prop.Method;
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


        db.SaveChanges();


        return true;
    }

    public bool DeleteVehicle(DeleteVehicleSchema prop)
    {
        using var db = new ParkingDb();
        var user = db.Users.AsEnumerable().FirstOrDefault(user => user.Email == prop.Email
                                                                  && user.Password == prop.Password);

        if (user is null) return false;
        var vehicle = db.Vehicles.Where(vehicle =>
            vehicle.LicensePlate == prop.LicensePlate && vehicle.UserId == user.Id);


        var buy = db.Buys.Where(buy => buy.VehicleIdentifier == prop.LicensePlate && buy.UserId == user.Id);

        foreach (var value in vehicle) db.Vehicles.Remove(value);

        foreach (var value in buy) db.Buys.Remove(value);


        db.SaveChanges();

        return true;
    }
}