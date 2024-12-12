using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace TruthOrDrink.Helpers
{
    internal class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32; 
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            var salt = new byte[SaltSize]; // maakt een lege byte array aan met de lengte van SaltSize
            RandomNumberGenerator.Fill(salt); // vult de salt byte array met random bytes

            // maakt een instance aan van Rfc2898DeriveBytes aan met de password, salt, iterations en het HashAlgoritme
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(KeySize); // haalt de hash op van het wachtwoord in vorm van bytes via de pbkdf2 instance

            var hashBytes = new byte[SaltSize + KeySize]; // maakt een nieuwe byte array om de salt en de hash bijeen te voegen
            Array.Copy(salt, 0, hashBytes, 0, SaltSize); // kopieert de salt naar de hashBytes array
            Array.Copy(hash, 0 , hashBytes, SaltSize, KeySize); // kopieert de hash naar de hashBytes array

            return Convert.ToBase64String(hashBytes); // zet de hashBytes om naar een base64 string en returned deze
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword); // Dit zet de base64 string van de hash om in een byte array

            var salt = new byte[SaltSize]; // maakt een lege byte array aan, net zoals bij het hashen
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(KeySize);

            for (int i = 0; i < KeySize; i++) // iterreer over de hash en kijk of deze overeenkomt met de hash in de hashBytes array
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
