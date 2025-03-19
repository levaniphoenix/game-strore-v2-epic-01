namespace Business.Models;

public class PaymentMethodsModel
{
	public PaymentMethod[] PaymentMethods => [
				new PaymentMethod(){ ImageUrl = "https://icons.veryicon.com/png/o/miscellaneous/the-font-is-great/bank-41.png", Title = "Bank", Description = "Pay with bank transfer" },
				new PaymentMethod(){ ImageUrl = "https://cdn4.iconfinder.com/data/icons/flat-brand-logo-2/512/visa-512.png", Title = "Visa", Description = "Pay with a visa card" },
				new PaymentMethod(){ ImageUrl = "https://www.shutterstock.com/image-illustration/payment-terminal-icon-outline-web-260nw-1389038699.jpg", Title = "IBox terminal", Description = "Pay from a IBox terminal" }
			];

	public class PaymentMethod
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
	}
}
