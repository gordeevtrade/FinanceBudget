using Microsoft.AspNetCore.Identity;

namespace Budget.DAL.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}