using Data.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Gamestore.Tests
{
	internal class GameEqualityComparer : IEqualityComparer<Game>
	{
		public bool Equals(Game? x, Game? y)
		{
			if (x == null && y == null)
				return true;
			if (x == null || y == null)
				return false;

			return x.Id == y.Id
				&& x.Name == y.Name
				&& x.Key == y.Key
				&& x.Description == y.Description;
		}

		public int GetHashCode([DisallowNull] Game obj)
		{
			return obj.GetHashCode();
		}
	}

	//GenreEqualityComparer
	internal class GenreEqualityComparer : IEqualityComparer<Genre>
	{
		public bool Equals(Genre? x, Genre? y)
		{
			if (x == null && y == null)
				return true;
			if (x == null || y == null)
				return false;

			return x.Id == y.Id
				&& x.Name == y.Name;
		}

		public int GetHashCode([DisallowNull] Genre obj)
		{
			return obj.GetHashCode();
		}
	}

	//platform comparer
	internal class PlatformEqualityComparer : IEqualityComparer<Platform>
	{
		public bool Equals(Platform? x, Platform? y)
		{
			if (x == null && y == null)
				return true;
			if (x == null || y == null)
				return false;

			return x.Id == y.Id
				&& x.Type == y.Type;
		}

		public int GetHashCode([DisallowNull] Platform obj)
		{
			return obj.GetHashCode();
		}
	}
}
