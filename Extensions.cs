using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ReShade_Centralized
{
    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                string directory = Path.GetDirectoryName(completeFileName);

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                if (file.Name != "")
                    file.ExtractToFile(completeFileName, true);
            }
            archive.Dispose();
        }
    }

    public static class DirectoryExtensions
    {
        public static void MoveAndOverwrite(string source, string dest, bool overwrite)
        {
            if (overwrite == false)
            {
                Directory.Move(source, dest);
                return;
            }
            //Directory.EnumerateFiles(source, )
            //foreach (DirectoryInfo info in DirectoryInfo.)
            //{

            //}
        }
    }
    
}
