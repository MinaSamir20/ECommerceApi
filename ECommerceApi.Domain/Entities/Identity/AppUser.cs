using Microsoft.AspNetCore.Identity;

namespace ECommerceApi.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public DateTime LastLoginTime { get; set; }
    }
}
