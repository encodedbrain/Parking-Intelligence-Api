namespace Parking_Intelligence_Api.Schemas.User;

public class DownloadPhotoId
{
    public DownloadPhotoId(int id)
    {
        Id = id;
    }

    public DownloadPhotoId()
    {
    }

    public int Id { get; set; }
}