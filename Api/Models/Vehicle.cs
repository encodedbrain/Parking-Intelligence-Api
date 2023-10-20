namespace Parking_Intelligence_Api.Models;

public class Vehicle
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
    public string? Status { get; internal  set; }
    public string? Species { get; internal set; }
    public int UserId { get; internal set; }
    public User? User { get; internal set; }
}