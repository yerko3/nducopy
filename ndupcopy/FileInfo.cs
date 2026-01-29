using System.IO;
using System;

namespace ndupcopy
{
    public class FileInfo1
    {
        public static long GetFileSize(string filePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el tamaño del archivo {ex.Message}");
                return -1;
            }
        }
        public string FindFilePath(string fileName, string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath, fileName, SearchOption.AllDirectories);
            if (files.Length > 0)
                return files[0];
            else
                return null;
        }
    }
}
