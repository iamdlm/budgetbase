using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBase.Core.Application.Interfaces.Persistence
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
