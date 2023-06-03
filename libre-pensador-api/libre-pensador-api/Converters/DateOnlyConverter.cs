using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace libre_pensador_api.Converters
{

    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? stringValue = reader.GetString();
            if (stringValue == null)
                return DateOnly.MinValue;
            return DateOnly.Parse(stringValue);
        }


        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

}
