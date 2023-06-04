using libre_pensador_api.Utilities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace libre_pensador_api.Converters
{
    public class HashConverter : ValueConverter<string, string>
    {
        public HashConverter() : base(
            v => HashConverter.Hash(v), // to data  
            v => v) // from data
        {
        }

        static string Hash(string value) => HashingUtility.HashPassword(value);
    }
}
