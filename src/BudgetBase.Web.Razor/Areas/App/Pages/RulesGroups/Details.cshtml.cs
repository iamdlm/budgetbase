using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.RulesGroups
{
    public class RulesGroupDetailsModel : RulesGroupPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRulesGroupService _rulesGroupService;

        public RulesGroupDetailsModel(
            IMapper mapper,
            ITransactionRulesGroupService rulesGroupService,
            ICategoryService categoryService,
            ITransactionRulesGroupOperatorService operatorService,
            IUserService userService) : base(categoryService, operatorService, userService)
        {
            _mapper = mapper;
            _rulesGroupService = rulesGroupService;
        }

        public RulesGroupViewModel RulesGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                TransactionRulesGroupDto dto = await _rulesGroupService.GetByIdAsync(id.Value).ConfigureAwait(false);

                if (dto == null)
                {
                    return NotFound();
                }
                else
                {
                    RulesGroup = _mapper.Map<RulesGroupViewModel>(dto);
                }
            }
            catch (ValidationException ex)
            {
                return NotFound();
            }

            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }
    }
}
