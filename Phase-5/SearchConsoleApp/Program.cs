using SearchEngine.Classes;

namespace SearchConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager("../../../IndexerConfig.json",
                                    "../../../DatabaseConfig.json");
            manager.Run();
        }
    }
}