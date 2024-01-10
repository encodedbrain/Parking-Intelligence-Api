namespace Parking_Intelligence_Api.Schemas.User;

public class GetProfileSchema
{
    public GetProfileSchema()
    {
    }

    public GetProfileSchema(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}