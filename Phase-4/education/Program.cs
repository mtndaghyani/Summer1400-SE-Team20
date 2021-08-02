using System;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace education
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText("resources/Scores.json");
            JObject json = JObject.Parse(content);
            Console.WriteLine(json);
        }
    }
}
