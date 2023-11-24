namespace Parking_Intelligence_Api.Schemas.vehicle;

public class GetVehicleSchema
{
    public GetVehicleSchema(string name)
    {
        Name = name;
    }

    public GetVehicleSchema()
    {
    }

    public string Name { get; set; } = null!;
}