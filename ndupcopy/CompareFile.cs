using System.IO;
using System;
using System.Collections;
using System.Security.Cryptography;

namespace ndupcopy
{
    public class CompareFile : IDisposable
    {
        public void Dispose()
        {
        }
        public bool CompareFileContents(string FilePath1, string FilePath2)
        {
            using (Stream s1 = File.OpenRead(FilePath1))
            using (Stream s2 = File.OpenRead(FilePath2))
            {
                if (s1.Length != s2.Length)
                    return false;

                byte[] buffer1 = new byte[2048];
                byte[] buffer2 = new byte[2048];

                while (true)
                {
                    int br1 = s1.Read(buffer1, 0, buffer1.Length);
                    int br2 = s2.Read(buffer2, 0, buffer2.Length);

                    if (br1 == 0 && br2 == 0)
                        break;

                    for (int i = 0; i < br1; i++)
                    {
                        if (buffer1[i] != buffer2[i])
                            return false;
                    }
                }
            }
            return true;
        }
        public bool CompareFileSizes(string FilePath1, string FilePath2)
        {
            return FileInfo1.GetFileSize(FilePath1) == FileInfo1.GetFileSize(FilePath2);
        }
        public bool AreFilesEqual(string filePath1, string filePath2)
        {
            try
            {
                string[] linesFile1 = File.ReadAllLines(filePath1);
                string[] linesFile2 = File.ReadAllLines(filePath2);

                if (linesFile1.Length != linesFile2.Length)
                    return false;

                for (int i = 0; i < linesFile1.Length; i++)
                {
                    if (linesFile1[i] != linesFile2[i])
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading the file: {ex.Message}");
                return false;
            }
        }
        public bool CompareFilesHashes(FileHash fileHash1, FileHash fileHash2)
        {
            if(fileHash1 == null || fileHash2 == null)
                return false;

            return fileHash1 == fileHash2;
        }
        public bool CompareFilesBinary(string filePath1, string filePath2)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream1 = File.OpenRead(filePath1))
                using (var stream2 = File.OpenRead(filePath2))
                {
                    var hash1 = md5.ComputeHash(stream1);
                    var hash2 = md5.ComputeHash(stream2);

                    return StructuralComparisons.StructuralEqualityComparer.Equals(hash1, hash2);
                }
            }   
        }

        public bool VerifyFiles(string filePath1, string filePath2)
        {
            
            if (!CompareFileSizes(filePath1, filePath2))
                return false;

            if (!CompareFileContents(filePath1, filePath2))
                return false;

            if (!AreFilesEqual(filePath1, filePath2))
                return false;

            if (!CompareFilesBinary(filePath1, filePath2))
                return false;

            FileHash fileHash1 = new FileHash(filePath1);
            FileHash fileHash2 = new FileHash(filePath2);
            if (!CompareFilesHashes(fileHash1, fileHash2))
                return false;

            // Si todas las comparaciones son consistentes, los archivos son iguales
            return true;
        }
    }   
}