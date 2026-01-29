using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ndupcopy
{
    internal class FileCopier : IDisposable
    {
        private bool disposed = false;
        private CompareFile _comparer = new CompareFile();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _comparer.Dispose();
                }
                disposed = true;
            }
        }

        ~FileCopier()
        {
            Dispose(false);
        }
        public void CopyUniqueFiles(List<string> files, string destinationFolder)
        {
            HashSet<string> existingFileNames = GetExistingFileNames(destinationFolder);
            
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destinationFilePath = Path.Combine(destinationFolder, fileName);

                if (FileExistsInDestination(fileName, existingFileNames))
                    continue;

                if (IsIdenticalFile(file, existingFileNames,destinationFolder))
                    continue;

                CopyFile(file, destinationFilePath);
                existingFileNames.Add(fileName);
            }
        }

        private HashSet<string> GetExistingFileNames(string destinationFolder)
        {
            return new HashSet<string>(Directory.GetFiles(destinationFolder).Select(Path.GetFileName));
        }

        private bool FileExistsInDestination(string fileName, HashSet<string> existingFileNames)
        {
            if (existingFileNames.Contains(fileName))
            {
                Console.WriteLine($"El archivo '{fileName}' ya existe en la carpeta de destino.");
                return true;
            }
            return false;
        }
        private bool IsIdenticalFile(string file, HashSet<string> existingFileNames, string destinationFolder)
        {
            string fileName = Path.GetFileName(file);
            string fileHash = FileHash.GetFileHash(file);

            foreach (string existingFile in existingFileNames)
            {
                string existingFilePath = Path.Combine(destinationFolder, existingFile);
                string existingFileHash = FileHash.GetFileHash(existingFilePath);

                if (fileHash == existingFileHash && !CompareFiles(file, existingFilePath))
                {
                    Console.WriteLine($"El archivo '{fileName}' es idéntico a '{existingFile}' en la carpeta de destino.");
                    return true;
                }
            }
            return false;
        }

        private void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                File.Copy(sourceFilePath, destinationFilePath);
                Console.WriteLine($"Archivo copiado: {Path.GetFileName(sourceFilePath)} -> {destinationFilePath}");
            }
            catch (IOException)
            {
                Console.WriteLine($"Error: El archivo '{Path.GetFileName(sourceFilePath)}' ya existe en la carpeta de destino.");
            }
        }

        private bool CompareFiles(string filePath1, string filePath2)
        {
            return _comparer.VerifyFiles(filePath1, filePath2);
        }
    }
}
