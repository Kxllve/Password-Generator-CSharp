using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator
{
    class Program
    {
        private const string CHAR_POOL = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!$%&*@#";
        private const int DEFAULT_LENGTH = 16;
        static void Main(string[] args)
        {
            int passwordLength = 16;

            Console.WriteLine("Welcome to the Password Generator!");
            Console.Write("Please enter a length (default: 16 characters): ");
            if (int.TryParse(Console.ReadLine(), out passwordLength)){
                GeneratePassword(passwordLength);
            }
        }

        static void GeneratePassword(int passwordLength)
        {
            if (passwordLength < 2)
            {
                passwordLength = DEFAULT_LENGTH;
            }

            using var rng = RandomNumberGenerator.Create();
            StringBuilder password = new StringBuilder();

            byte[] randomBytes = new byte[passwordLength];
            rng.GetBytes(randomBytes);

            for (int i = 0; i < passwordLength; i++)
            {
                int currentByte = randomBytes[i] % 69;
                char currentChar = CHAR_POOL[currentByte];
                password.Append(currentChar);
            }

            Console.WriteLine(password);
            TextCopy.ClipboardService.SetText(password.ToString());
            Console.WriteLine("Copied the password to your clipboard.");
            Console.WriteLine("WARNING: Clipboard is not secure. Other apps can read it!");
        }
    }
}