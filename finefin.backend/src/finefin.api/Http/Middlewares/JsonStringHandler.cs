using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace finefin.api.Http.Middlewares
{
    public partial class JsonStringHandler : JsonConverter<string>
    {
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString()?.Trim();

            if (value == null)
                return null;

            return RemoveExtraWhiteSpaces().Replace(value, " ");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => writer.WriteStringValue(value);

        [GeneratedRegex(@"\s+")]
        private static partial Regex RemoveExtraWhiteSpaces();
    }
}
