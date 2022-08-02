namespace WebApiProductCRUD.Models
{
    public interface IAppEntity
    {
        string Id { get; set; }
        DateTime CreatedUtc { get; set; }
        DateTime LastUpdateUtc { get; set; }
        bool Deleted { get; set; }
    }
}
