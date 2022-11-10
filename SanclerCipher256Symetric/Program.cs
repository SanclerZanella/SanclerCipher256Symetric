using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanclerCipher256Symetric
{
    internal class Program
    {

        // Create a Top secret text file. Save it to the Desktop.
        // Encrypt is using a symetric key
        // Decrypt it using the symetric key
        static void Main(string[] args)
        {

            // Get the filepath to save the top secret text file.
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Using the desktop path append the file name to create the full file path
            string filePath = desktop + "\\TopSecret.txt";

            // Get user's input
            Console.WriteLine("Please enter your secret message and press enter:");
            Console.ForegroundColor = ConsoleColor.Red;
            string secretMessage = Console.ReadLine();

            // Write user's input in the created file
            File.WriteAllText(filePath, secretMessage);

            // Show the user what message is saved in the file
            Console.ResetColor();
            Console.WriteLine("\n\nThe TopSecret file contains the message:");
            Console.ForegroundColor = ConsoleColor.Green;
            string unEncryptedMessage = File.ReadAllText(filePath);
            Console.WriteLine(unEncryptedMessage);

            // Ask the user to set the key to encrypt and decrypt the file
            Console.ResetColor();
            Console.WriteLine("\n\nPlease provide the encryption/decryption key:");
            Console.ForegroundColor = ConsoleColor.Red;
            string key = Console.ReadLine();

            // Encrypt the file
            Console.ResetColor();
            string encryptedMessage = StringCipher.Encrypt(secretMessage, key);
            File.WriteAllText(filePath, encryptedMessage);

            Console.WriteLine("\n\nThe secret message has been encrypted to:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(encryptedMessage);
            Console.ResetColor();

            Console.WriteLine("\n\nThe TopSecret file contains the message:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(File.ReadAllText(filePath));
            Console.ResetColor();

            // Dencrypt the file
            Console.WriteLine("\n\nPress enter to decrypt the file:");
            Console.ReadLine();

            Console.WriteLine("\n\nEnter the key to decrypt the file:");
            Console.ForegroundColor = ConsoleColor.Green;
            string decryptKey = Console.ReadLine();

            string decryptedMessage = StringCipher.Decrypt(encryptedMessage, decryptKey);
            Console.WriteLine(decryptedMessage);
            File.WriteAllText(filePath, decryptedMessage);
            Console.WriteLine("\n\n The Top Secret file contains the message:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(File.ReadAllText(filePath));
            Console.ResetColor();

        }
    }
}
