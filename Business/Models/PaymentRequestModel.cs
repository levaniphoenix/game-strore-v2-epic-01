namespace Business.Models;

public class PaymentRequestModel
{
	public string Method { get; set; } = string.Empty;
	public dynamic? Model { get; set; }
}
