namespace Business.Exceptions
{
	public class GameStoreModelStateException : GameStoreException
	{
		public GameStoreModelStateException(string message) : base(message)
		{
		}

		public GameStoreModelStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public GameStoreModelStateException()
		{
		}
	}
}
