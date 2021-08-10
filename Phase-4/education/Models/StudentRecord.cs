using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace education
{
    public class StudentRecord
    {
        public string FirstName { get; }
        public string Lastname { get; }
        public double Average { get; }

        public StudentRecord(string firstName, string lastname, double average)
        {
            FirstName = firstName;
            Lastname = lastname;
            Average = average;
        }

        public static List<StudentRecord> GetTopStudentsRecords(int topStudentsCount, List<StudentAveragePair> averagePairs, List<Student> students)
        {
            var topStudents =  averagePairs.Join(
                    students,
                    x => x.StudentNumber, y => y.StudentNumber,
                    (q, w) => new StudentRecord(w.FirstName, w.LastName, q.Average))
                .OrderByDescending(q => q.Average);
            return topStudents.Take(topStudentsCount).ToList();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var info in this.GetType().GetProperties())
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }
            return sb.ToString();
        }
    }
}