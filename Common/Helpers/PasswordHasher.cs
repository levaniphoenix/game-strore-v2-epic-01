using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers;

public static class PasswordHasher
{
	private const int HashSize = 32; // SHA512 hash size in bytes
	private const int SaltSize = 16; // Salt size in bytes
	private const int Iterations = 100000;

	public static string HashPassword(string password)
	{
		var passwordBytes= Encoding.UTF8.GetBytes(password);
		byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
		byte[] hash = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt,Iterations, HashAlgorithmName.SHA512, HashSize);

		return Convert.ToBase64String(hash) + "-" + Convert.ToBase64String(salt);
	}

	public static bool VerifyHashedPassword(string hashedPassword, string password)
	{
		string[] parts = hashedPassword.Split('-');
		if (parts.Length != 2)
		{
			throw new FormatException("Invalid hash format");
		}
		byte[] hash = Convert.FromBase64String(parts[0]);
		byte[] salt = Convert.FromBase64String(parts[1]);

		byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, Iterations, HashAlgorithmName.SHA512, HashSize);

		return CryptographicOperations.FixedTimeEquals(inputHash, hash);
	}
}
