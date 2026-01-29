using System.Collections.Generic;
using System.IO;


namespace ndupcopy
{
    public class FileReader
    {
        public delegate int FileComparisonDelegate(string file1, string file2);
        private List<string> _files = new List<string>();
        private List<string> _sourceDirectories = new List<string>();


        public void AddSourceDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"El directorio '{directoryPath}' no existe.");

            _sourceDirectories.Add(directoryPath);
        }

        public void AddFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"El archivo '{filePath}' no existe.");

            _files.Add(filePath);
        }

        public void RemoveFile(string filePath)
        {
            _files.Remove(filePath);
        }
        public void SortFiles(FileComparisonDelegate comparison)
        {
            int n = _files.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (comparison(_files[j], _files[i]) < 0)
                    {
                        Swap(i, j);
                    }
                }
            }
        }
        private void Swap(int i,int j)
        {
            string temp = _files[i];
            _files[i] = _files[j];
            _files[j] = temp;
        }
        public void ReadFilesFromSourceDirectories()
        {
            foreach (string sourceDir in _sourceDirectories)
            {
                string[] files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
                _files.AddRange(files);
            }
        }

        public List<string> GetFiles()
        {
            return new List<string>(_files);
        }

    }
}
