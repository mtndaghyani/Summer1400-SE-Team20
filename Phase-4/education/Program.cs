namespace education
{
    static class Program
    {
        private const string SlnPath = "../../../";
        private const string ScorePath = SlnPath + "resources/Scores.json";
        private const string StudentsPath = SlnPath + "resources/Students.json";
        private const int TopStudentsCount = 3;

        private static void Main(string[] args)
        {
            Manager.Run(ScorePath, StudentsPath, TopStudentsCount);
        }
    }
}