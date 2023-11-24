namespace Parking_Intelligence_Api.Schemas.vehicle;

public class UpdateVehicleSchema
{


    public UpdateVehicleSchema()
    {
        
    }

    public UpdateVehicleSchema(string email, string password, string vehicleIdentifier , string method)
    {
        Email = email;
        Password = password;
        VehicleIdentifier = vehicleIdentifier;
        Method = method;
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VehicleIdentifier { get; set; } = null!;
    public string Method { get; set; } = null!;
}