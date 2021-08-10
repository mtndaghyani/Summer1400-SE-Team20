using System.Collections.Generic;
using System.Linq;

namespace education
{
    public class StudentAveragePair
    {
        public int StudentNumber { get; }

        public double Average { get; }

        private StudentAveragePair(int studentNumber, double average)
        {
            StudentNumber = studentNumber;
            Average = average;
        }
        
        public static List<StudentAveragePair> GetAverageList(List<ScoreSheet> scores)
        {
            return scores.GroupBy(
                x => x.StudentNumber,
                x => x.Score,
                (key, val) => new StudentAveragePair(key, val.Average())).ToList();
        }
    }
}
