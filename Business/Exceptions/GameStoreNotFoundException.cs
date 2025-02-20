using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
	public class GameStoreNotFoundException : GameStoreException
	{
		public GameStoreNotFoundException() { }

		public GameStoreNotFoundException(string message) : base(message) { }

		public GameStoreNotFoundException(string message, Exception innerException) : base(message, innerException) { }
	}
}
