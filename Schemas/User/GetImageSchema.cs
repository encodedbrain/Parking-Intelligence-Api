namespace Parking_Intelligence_Api.Schemas.User;

public class GetImageSchema
{
    public GetImageSchema(int id)
    {
        Id = id;
    }

    public GetImageSchema()
    {
    }

    public int Id { get; set; }
}