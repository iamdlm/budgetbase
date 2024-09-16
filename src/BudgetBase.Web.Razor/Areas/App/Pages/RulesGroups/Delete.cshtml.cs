﻿using AutoMapper;
using BudgetBase.Core.Application.DTOs.Persistence;
using BudgetBase.Core.Application.Exceptions;
using BudgetBase.Core.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using BudgetBase.Web.Razor.Areas.App.ViewModels.RulesGroup;
using BudgetBase.Core.Application.Services.Persistence;

namespace BudgetBase.Web.Razor.Areas.App.Pages.RulesGroups
{
    public class RulesGroupDeleteModel : RulesGroupPageModelBase
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRulesGroupService _rulesGroupService;

        public RulesGroupDeleteModel(
            IMapper mapper,
            ITransactionRulesGroupService rulesGroupService,
            ICategoryService categoryService,
            ITransactionRulesGroupOperatorService operatorService) : base(categoryService, operatorService)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                await _rulesGroupService.DeleteAsync(id.Value).ConfigureAwait(false);
            }
            catch (ValidationException ex)
            {
            }

            return RedirectToPage("./Index");
        }
    }
}
