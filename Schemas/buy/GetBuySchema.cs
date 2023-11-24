namespace Parking_Intelligence_Api.Schemas.buy;

public class GetBuySchema
{
    public GetBuySchema()
    {
    }

    public GetBuySchema(string name)
    {
        Name = name;
    }

    public string Name { get; set; } = null!;
}