using System.Globalization;
namespace Common.Filters;

public class OrderHistoryFilter
{
	public string? Start { get; set; }

	public string? End { get; set; }

	public static DateTime? ParseDate(string? raw)
	{
		if (string.IsNullOrWhiteSpace(raw))
			return null;

		try
		{
			// Remove anything in parentheses
			int parenIndex = raw.IndexOf('(');
			if (parenIndex != -1)
				raw = raw[..parenIndex].Trim();

			raw = raw.Replace("GMT ", "GMT+");

			// Expected format: "Tue Apr 08 2025 00:00:00 GMT+0400"
			string format = "ddd MMM dd yyyy HH:mm:ss 'GMT'K";

			// Parse as DateTimeOffset and return the UTC DateTime
			var dto = DateTimeOffset.ParseExact(raw, format, CultureInfo.InvariantCulture);
			return dto.UtcDateTime;
		}
		catch
		{
			return null;
		}
	}
}
