using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetBase.Core.Application.DTOs.Application
{
    public class ChartPLOptions
    {
        public List<ChartPLOptionsSerie> Series { get; set; } = new List<ChartPLOptionsSerie>();
        public ChartPLXAxis Xaxis { get; set; } = new ChartPLXAxis();
    }
}