using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.RequestModels
{
    public class PeriodIncomeRequest
    {
        public DateOnly PeriodStart { get; set; }
        public DateOnly PeriodEnd { get; set;}
    }
}
