using System;
using System.IO;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace education
{
    class ScoreSheet
    {
        public int StudentNumber{get; set;}
        public string Lesson{get; set;}
        public double Score{get; set;}
    }

    class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText("resources/Scores.json");
            // List<Dictionary<string, string>> Scores = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(content);
            List<ScoreSheet> Scores = JsonConvert.DeserializeObject<List<ScoreSheet>>(content);
            foreach(var x in Scores){
                Console.WriteLine(x.Lesson);
            }

            var MyList = Scores.GroupBy(x => x.StudentNumber,
                            x => x.Score,
                            (key, val) => new {StudentNumber=key, Average=val.ToList().Average()}).ToList();

            foreach(var x in MyList){
                Console.WriteLine(x);
            }
            /*
            foreach(var dictionary in Scores){
                var asString = string.Join(Environment.NewLine, dictionary);
                Console.WriteLine(asString);
            }
            */
        }
    }
}
