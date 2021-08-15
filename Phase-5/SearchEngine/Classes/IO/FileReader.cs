using System.Collections.Generic;
using System.IO;
using System.Linq;
using SearchEngine.Interfaces.IO;

namespace SearchEngine.Classes.IO
{
    public class FileReader : IReader
    {
        private readonly string _path;

        public FileReader(string path)
        {
            _path = path;
        }

        public List<string> Read()
        {
           return Directory.EnumerateFiles(_path)
                                             .Select(File.ReadAllText)
                                             .ToList();
        }
    }
}