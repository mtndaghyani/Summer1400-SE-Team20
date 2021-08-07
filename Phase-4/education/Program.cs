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

            var averageList = StudentAveragePair.GetAverageList(scores);
            var topStudents = StudentRecord.GetTopStudentsRecords(TopStudentsCount, averageList, students);
            
            PrintEnumerable(topStudents);
        }

        private static void PrintEnumerable(List<StudentRecord> topStudents)
        {
            foreach (var x in topStudents)
            {
                Console.WriteLine(x);
            }
        }
    }
}