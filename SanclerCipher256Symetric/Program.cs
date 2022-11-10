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
            string secretMessage = Console.ReadLine();

            // Write user's input in the created file
            File.WriteAllText(filePath, secretMessage);

        }
    }
}
