using System;
using System.IO;

namespace Fds
{
    public class FdsFile
    {

        public long FileSize { get; }
        public string FileName { get; }
        public string FilePath { get; }
        public bool HasHeader { get; }
        public bool Corrupt { get; }
        public long Sides { get; }

        public string Header { get; }

        private byte[] _data;

        private byte[] _header;



        public FdsFile(string filePath)
        {
            var file = new FileInfo(filePath);
            FilePath = file.FullName;
            FileSize = file.Length;
            FileName = file.Name;
            Sides = FileSize / 65500;
            HasHeader = FileSize % 65500 == 16;
            Corrupt = !HasHeader && FileSize % 65500 != 0;
            Header = FdsUtility.GetHeader(Sides);
            if (!HasHeader && !Corrupt)
            {
                ReadFileAndGenerateHeader();
            }

        }

        private void ReadFileAndGenerateHeader()
        {
            _data = File.ReadAllBytes(FilePath);
            _header = FdsUtility.GenerateHeader(Sides);
        }

        public void WriteFile()
        {
            if (!HasHeader && !Corrupt)
            {
                File.Copy(FilePath, FilePath.Replace(".fds", "_noheader.fds"));
                File.Delete(FilePath);
                FdsUtility.ByteArrayToFile(FilePath, FdsUtility.Combine(_header, _data));
                Console.WriteLine("File written.");
                return;
            }
            Console.WriteLine("File has header or is corrupt/not a valid fds image");

        }

        public void WriteFile(FdsWriteOptions option, string filenameAppend)
        {
            if (HasHeader || Corrupt)
            {
                Console.WriteLine("File has header or is corrupt/not a valid fds image");
                return;
            }
            if (option == FdsWriteOptions.WriteNewFile)
            {
                FdsUtility.ByteArrayToFile(
                    FilePath.Replace(".fds", filenameAppend + ".fds"),
                    FdsUtility.Combine(_header, _data));
                Console.WriteLine("File Written");
            }
            if (option == FdsWriteOptions.BackupOriginal)
            {
                File.Copy(FilePath, FilePath.Replace(".fds", filenameAppend + ".fds"));
                Console.WriteLine("Original backed up");
            }
            if (option == FdsWriteOptions.ModifyInPlace)
            {
                File.Delete(FilePath);
                FdsUtility.ByteArrayToFile(FilePath, FdsUtility.Combine(_header, _data));
                Console.WriteLine("File Written");
            }
        }

    }
}
