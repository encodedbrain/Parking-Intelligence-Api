namespace Parking_Intelligence_Api.Schemas.User;

public class DownloadPhotoIdSchema
{
    public DownloadPhotoIdSchema(int id)
    {
        Id = id;
    }

    public DownloadPhotoIdSchema()
    {
    }

    public int Id { get; set; }
}