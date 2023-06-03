using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace libre_pensador_api.Converters
{
    public class EncryptionConverter : ValueConverter<string?, string?>
    {
        public EncryptionConverter() : base(
            v => Encrypt(v), // to data
            v => Decrypt(v)) // from data
        {
        }

        static string? Encrypt(string? value) => EncryptionUtility.Encrypt(value);
        static string? Decrypt(string? value) => EncryptionUtility.Decrypt(value);
    }

}
