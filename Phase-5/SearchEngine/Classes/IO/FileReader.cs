using System;
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

        public IEnumerable<string> Read()
        {
            foreach (var fileName in Directory.EnumerateFiles(_path).OrderBy(x => x))
            {
                Console.WriteLine($"Read file {fileName}");
                yield return File.ReadAllText(fileName);
            }
        }
    }
}