using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.RequestModels
{
    public class ProfitOfPeriodRequest
    {
        public enum TimeLapses { Day = 2, Month = 1, Year = 0 }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public TimeLapses PeriodDivision { get; set; }
    }
}
