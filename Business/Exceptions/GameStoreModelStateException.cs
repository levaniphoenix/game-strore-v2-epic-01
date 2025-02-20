using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
