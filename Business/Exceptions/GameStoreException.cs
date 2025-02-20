namespace Business.Exceptions
{
	public abstract class GameStoreException : Exception
	{
		protected GameStoreException() { }

		protected GameStoreException(string message) : base(message) { }

		protected GameStoreException(string message, Exception innerException) : base(message, innerException) { }
	}
}
