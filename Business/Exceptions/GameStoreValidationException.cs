namespace Business.Exceptions
{
	public class GameStoreValidationException : GameStoreException
	{
		public GameStoreValidationException(string message) : base(message)
		{
		}

		public GameStoreValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public GameStoreValidationException()
		{
		}
	}
}
