using Newtonsoft.Json;

public class UnixMillisecondsDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Null:
                if (objectType == typeof(DateTime?))
                    return default;
                throw new JsonSerializationException("Cannot convert null value to DateTime.");

            case JsonToken.Integer:
                var milliseconds = (long)reader.Value!;
                return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;

            case JsonToken.String:
                return DateTime.Parse((string)reader.Value!);

            case JsonToken.Date:
                return (DateTime)reader.Value!;

            default:
                throw new JsonSerializationException($"Unexpected token type {reader.TokenType} when parsing date.");
        }
    }

    public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
    {
        long milliseconds = new DateTimeOffset(value).ToUnixTimeMilliseconds();
        writer.WriteValue(milliseconds);
    }
}
