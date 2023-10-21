namespace Parking_Intelligence_Api.Schemas.vehicle;

public class UpdateVehicleSchema
{


    public UpdateVehicleSchema()
    {
        
    }

    public UpdateVehicleSchema(string email, string password, string vehicleIdentifier)
    {
        Email = email;
        Password = password;
        VehicleIdentifier = vehicleIdentifier;
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VehicleIdentifier { get; set; } = null!;
}