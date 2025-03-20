using System.Text.Json;

namespace Business.Models;

public class PaymentRequestModel
{
	public string Method { get; set; } = string.Empty;
	public JsonElement Model { get; set; }
}
