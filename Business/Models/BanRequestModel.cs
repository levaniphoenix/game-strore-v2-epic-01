using System.Text.Json.Serialization;

namespace Business.Models;
public class BanRequestModel
{
	[JsonPropertyName("user")]
	public string UserName { get; set; }

	[JsonPropertyName("duration")]
	public string Duration { get; set; }
}
