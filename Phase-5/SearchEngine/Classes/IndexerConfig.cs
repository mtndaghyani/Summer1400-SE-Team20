using System.IO;
using Newtonsoft.Json;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class IndexerConfig
    {
        public bool IndexToDb { get; set; }
        public bool DoIndex { get; set; }
        public string DataPath { get; set; }
    }
}