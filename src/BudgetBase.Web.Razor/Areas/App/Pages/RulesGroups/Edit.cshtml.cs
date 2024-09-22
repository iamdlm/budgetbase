using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup;
using BudgetBase.Core.Domain.Models;
using BudgetBase.Web.Razor.Helpers;
using BudgetBase.Core.Application.Interfaces.Identity;

namespace BudgetBase.Web.Razor.Areas.App.Pages.RulesGroups
{
    public class RulesGroupEditModel : RulesGroupPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRulesGroupService _rulesGroupService;

        public RulesGroupEditModel(
            IMapper mapper,
            ITransactionRulesGroupService rulesGroupService,
            ICategoryService categoryService,
            ITransactionRulesGroupOperatorService operatorService,
            IUserService userService) : base(categoryService, operatorService, userService)
        {
            _mapper = mapper;
            _rulesGroupService = rulesGroupService;
        }

        [BindProperty]
        public RulesGroupViewModel RulesGroup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TransactionRulesGroupDto dto = await _rulesGroupService.GetByIdAsync(id.Value).ConfigureAwait(false);

            if (dto == null)
            {
                return NotFound();
            }
            else
            {
                RulesGroup = _mapper.Map<RulesGroupViewModel>(dto);
            }

            await SetSelectListsAsync().ConfigureAwait(false);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            try
            {
                TransactionRulesGroupDto dto = _mapper.Map<TransactionRulesGroupDto>(RulesGroup);
                await _rulesGroupService.UpdateAsync(dto).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
                ModelState.AddValidationErrorsToModelState(ex.Errors);
                await SetSelectListsAsync().ConfigureAwait(false);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
