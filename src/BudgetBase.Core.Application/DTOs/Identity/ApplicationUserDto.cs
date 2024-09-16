using BudgetBase.Core.Application.DTOs.Persistence;

namespace BudgetBase.Core.Application.DTOs.Identity
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string FullName { get; set; }
        public Guid? CountryId { get; set; }
        public virtual CountryDto? Country { get; set; }
    }
}
