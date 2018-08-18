using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fds
{
    public static class FdsUtility
    {

        public const string HeaderStart = "46 44 53 1A 0"; //FDS EOF

        public const string HeaderEnd = " 00 00 00 00 00 00 00 00 00 00 00";

        public static bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }

        }

        public static string GetHeader(long sides)
        {
            return HeaderStart + sides + HeaderEnd;

        }

        public static byte[] GenerateHeader(long sides)
        {
            var headerstring = HeaderStart + sides + HeaderEnd;
            headerstring = headerstring.Replace(" ", string.Empty);
            return Enumerable.Range(0, headerstring.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(headerstring.Substring(x, 2), 16))
                .ToArray();


        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }


    }
}
