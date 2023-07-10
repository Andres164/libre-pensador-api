using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static SharedModels.RequestModels.ProfitOfPeriodRequest;

namespace SharedModels.RequestModels
{
    public class ProfitOfPeriodRequest
    {
        public enum TimeLapses { Day, Month, Year }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public TimeLapses PeriodDivision { get; set; }
        public static Dictionary<TimeLapses, string> TimeLapseTranslations { get; } = new Dictionary<TimeLapses, string>()
        {
            { TimeLapses.Day, "Dia" },
            { TimeLapses.Month, "Mes" },
            { TimeLapses.Year, "Año" }
        };

        public static DateTime AddTimeLapseToDate(DateTime date, TimeLapses timeLapseToAdd, int amount)
        {
            return timeLapseToAdd switch
            {
               TimeLapses.Day => date.AddDays(amount),
               TimeLapses.Month => date.AddMonths(amount),
               TimeLapses.Year => date.AddYears(amount),
               _ => throw new ArgumentException("The given TimeLapse is not valid")
            };
        }

        public static string GetTimeLapseTranslation(TimeLapses timeLapse)
        {
            if (TimeLapseTranslations.TryGetValue(timeLapse, out string? translation))
                return translation;

            throw new ArgumentException($"The timelapse '{timeLapse}' doesn't have a translation");
        }

        public static List<string> GetEveryTimeLapseTranslation()
        {
            return TimeLapseTranslations.Values.ToList();
        }

        public static TimeLapses GetTimeLapseFromTranslatedString(string translatedStringTimeLampse)
        {
            var keyValuePair = TimeLapseTranslations.FirstOrDefault(x => x.Value == translatedStringTimeLampse);
            if(keyValuePair.Equals(default(KeyValuePair<TimeLapses, string>)))
                throw new ArgumentException($"The string '{translatedStringTimeLampse}' doesn't have a corresponding TimeLapse enum value");

            return keyValuePair.Key;
        }
    }
}
