using System.IO;
using Newtonsoft.Json;

namespace SearchEngine.Classes
{
    public class Config
    {
        public bool IndexFromDb { get; set; }
        public bool DoIndex { get; set; }
        public string DataPath { get; set; }

        public string DatabaseProvider { get; set; }
        public static Config ReadConfig(string jsonPath)
        {
            string jsonContent = File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<Config>(jsonContent);
        }
    }
}