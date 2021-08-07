using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace education
{
    public static class Manager
    {
        public static void Run(string scorePath, string studentsPath, int topStudentsCount)
        {
            var scoresContent = File.ReadAllText(scorePath);
            var scores = JsonConvert.DeserializeObject<List<ScoreSheet>>(scoresContent);
            var studentsContent = File.ReadAllText(studentsPath);
            var students = JsonConvert.DeserializeObject<List<Student>>(studentsContent);

            var averageList = StudentAveragePair.GetAverageList(scores);
            var topStudents = StudentRecord.GetTopStudentsRecords(topStudentsCount, averageList, students);

            PrintElements(topStudents);
        }

        private static void PrintElements(List<StudentRecord> elements)
        {
            foreach (var x in elements)
            {
                Console.WriteLine(x);
            }
        }
    }
}