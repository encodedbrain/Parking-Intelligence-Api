namespace Parking_Intelligence_Api.Schemas;

public class DeleteVehicleSchema : LoginSchema
{
    public DeleteVehicleSchema(string email, string password, string licensePlate) : base(email, password)
    {
        this.LicensePlate = licensePlate;
    }

    public string LicensePlate { get; set; }
}