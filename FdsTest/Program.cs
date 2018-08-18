using System;
using System.IO;
using System.Linq;
using Fds;

namespace FdsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePaths = Directory.GetFiles(Environment.CurrentDirectory, "*.fds");

            foreach (var fdsFile in filePaths.Select(filePath => new FdsFile(filePath)))
            {
                Console.WriteLine($"Name : {fdsFile.FileName}");
                Console.WriteLine($"Size : {fdsFile.FileSize}");
                Console.WriteLine($"Number of sides : {fdsFile.Sides}");
                Console.WriteLine($"Has FDS header : {fdsFile.HasHeader}");
                Console.WriteLine($"Corrupt : {fdsFile.Corrupt}");
                Console.WriteLine($"Header : {fdsFile.Header}");
                fdsFile.WriteFile();
            }

            Console.ReadKey();
        }
    }
}
