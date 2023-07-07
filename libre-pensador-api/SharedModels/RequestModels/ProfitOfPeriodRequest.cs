using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.RequestModels
{
    public class ProfitOfPeriodRequest
    {
        public enum TimeLapses { Day, Month, Year }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public TimeLapses PeriodDivision { get; set; }

        public static DateTime GetSubstractTimeLapseToDate(DateTime date, TimeLapses timeLapseToSubstract)
        {
            return timeLapseToSubstract switch
            {
               TimeLapses.Day => date.AddDays(-1),
               TimeLapses.Month => date.AddMonths(-1),
               TimeLapses.Year => date.AddYears(-1),
               _ => throw new ArgumentException("The given TimeLapse is not valid")
            };
        }
    }
}
