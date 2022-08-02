using Microsoft.AspNetCore.Identity;

namespace WebApiProductCRUD.Models
{
    public class DbUser : IdentityUser, IAppEntity
    {
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdateUtc { get; set; } = DateTime.UtcNow;
        public bool Deleted { get; set; }

        public string? Name { get; set; }
        public string? NickName { get; set; }
        public string? Document { get; set; }
    }
}
