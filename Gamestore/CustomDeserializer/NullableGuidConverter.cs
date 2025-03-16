using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gamestore.CustomDeserializer;

public class NullableGuidConverter : JsonConverter<Guid?>
{
	public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			string? value = reader.GetString();
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}

			if (Guid.TryParse(value, out var guidValue))
			{
				return guidValue;
			}

			throw new JsonException($"Invalid GUID format: {value}");
		}
		else if (reader.TokenType == JsonTokenType.Null)
		{
			return null;
		}

		throw new JsonException("Unexpected token type for Guid?");
	}

	public override void Write(Utf8JsonWriter writer, Guid? value, JsonSerializerOptions options)
	{
		if (value.HasValue)
		{
			writer.WriteStringValue(value.Value.ToString());
		}
		else
		{
			writer.WriteNullValue();
		}
	}
}
