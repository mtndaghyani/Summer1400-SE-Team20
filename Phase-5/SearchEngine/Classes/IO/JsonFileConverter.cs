using System.IO;
using Newtonsoft.Json;

namespace SearchEngine.Classes.IO
{
    public static class JsonFileConverter
    {
        public static T ReadConfig<T>(string jsonPath)
        {
            string jsonContent = File.ReadAllText(jsonPath);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }
    }
}