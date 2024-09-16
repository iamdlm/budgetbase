using AutoMapper;
using BudgetBase.Core.Application.DTOs.Identity;
using BudgetBase.Infrastructure.Identity.Models;

namespace BudgetBase.Infrastructure.Identity.MappingProfiles
{
    public class InfrastructureIdentityProfile : Profile
    {
        public InfrastructureIdentityProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ReverseMap();
        }
    }
}
