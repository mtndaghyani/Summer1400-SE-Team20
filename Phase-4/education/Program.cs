using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace education
{
    class Program
    {
        private const string SlnPath = "../../../";
        private const string ScorePath = SlnPath + "resources/Scores.json";
        private const string StudentsPath = SlnPath + "resources/Students.json";
        private const int TopStudentsCount = 3;

        private static void Main(string[] args)
        {
            var scoresContent = File.ReadAllText(ScorePath);
            var scores = JsonConvert.DeserializeObject<List<ScoreSheet>>(scoresContent);
            var studentsContent = File.ReadAllText(StudentsPath);
            var students = JsonConvert.DeserializeObject<List<Student>>(studentsContent);

            var averageList = scores.GroupBy(
                x => x.StudentNumber,
                x => x.Score,
                (key, val) => new {StudentNumber = key, Average = val.ToList().Average()}).ToList();

            var topStudents = averageList.Join(
                    students,
                    x => x.StudentNumber, y => y.StudentNumber,
                    (q, w) => new {Average = q.Average, FirstName = w.FirstName, LastName = w.LastName})
                .OrderByDescending(q => q.Average)
                .ToList();

            topStudents = topStudents.Take(TopStudentsCount).ToList();
            foreach (var x in topStudents)
            {
                Console.WriteLine(x);
            }
        }
    }
}