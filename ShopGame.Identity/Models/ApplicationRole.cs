using Microsoft.AspNetCore.Identity;

namespace ShopGame.Identity.Models;

public class ApplicationRole:IdentityRole<int>
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}