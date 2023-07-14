using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SharedModels.RequestModels.ProfitOfPeriodRequest;

namespace SharedModels.Models
{
    public class ProfitOfPeriod
    {
        public decimal IncomeBeforeTaxes { get; set; } = 0;
        public decimal NetIncome { get; set; } = 0;
        public TimeLapses PeriodDuration { get; set; }
        public DateTime PeriodDate { get; set; }

        public string ShortenedPeriodDate
        {
            get
            {
                return this.PeriodDuration switch
                {
                    TimeLapses.Day => this.PeriodDate.ToString("dd/MMM"),
                    TimeLapses.Month => this.PeriodDate.ToString("MMMM"),
                    TimeLapses.Year => this.PeriodDate.ToString("yyyy"),
                    _ => throw new Exception($"The timelaps '{this.PeriodDuration}' doesn't have a conversion in ShortenedPeriodDate")
                };
            }
        }

        public decimal RoundedNetIncome
        {
            get
            {
                return Math.Round(this.NetIncome, 2);
            }
        }
    }
}
