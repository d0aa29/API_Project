using Microsoft.AspNetCore.Identity;

namespace MyAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }

}
