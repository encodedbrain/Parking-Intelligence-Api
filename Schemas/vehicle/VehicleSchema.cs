namespace Parking_Intelligence_Api.Schemas;

public class VehicleSchema
{
    public VehicleSchema(string model, string color, int year, string brand, string licensePlate, string species, string name)
    {
        this.Model = model;
        this.Color = color;
        this.Year = year;
        this.Brand = brand;
        this.LicensePlate = licensePlate;
        this.Species = species;
        this.Name = name;
    }

    public string Model  { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public string Brand { get; set; }
    public string LicensePlate { get; set; }
    public string Species { get; set; }
    public string Name { get; set; }

}