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

    class Student
    {
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Program
    {
        private const string SlnPath = "../../../";
        private const string ScorePath = SlnPath + "resources/Scores.json";
        private const string StudentsPath = SlnPath + "resources/Students.json";

        static void Main(string[] args)
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            string scoresContent = File.ReadAllText(ScorePath);
            List<ScoreSheet> scores = JsonConvert.DeserializeObject<List<ScoreSheet>>(scoresContent);
            string studentsContent = File.ReadAllText(StudentsPath);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(studentsContent);

            var averageList = scores.GroupBy(x => x.StudentNumber,
                    x => x.Score,
                    (key, val) => 
                        new {StudentNumber=key, Average=val.ToList().Average()}).ToList();

            var topStudents = averageList.Join(students,
                    x => x.StudentNumber, y => y.StudentNumber,
                    (q, w) =>
                    new {Average=q.Average, FirstName=w.FirstName, LastName=w.LastName})
                    .OrderByDescending(q => q.Average).ToList();

            topStudents = topStudents.Take(3).ToList();
            foreach (var x in topStudents)
            {
                Console.WriteLine(x);
            }
        }
    }
}
