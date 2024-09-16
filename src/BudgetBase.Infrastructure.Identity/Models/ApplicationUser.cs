using Microsoft.AspNetCore.Identity;

namespace BudgetBase.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? ProfilePic { get; set; }
        public Guid? CountryId { get; set; }
    }
}