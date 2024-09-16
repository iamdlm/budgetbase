using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBase.Core.Application.DTOs.Application
{
    public class ChartPLTimeSeries
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Categories { get; set; }
    }
}
