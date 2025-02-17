namespace Business.Exceptions
{
	public class GameStoreException : Exception
	{
		public GameStoreException() { }

		public GameStoreException(string message) : base(message) { }

		public GameStoreException(string message, Exception innerException) : base(message, innerException) { }
	}
}
