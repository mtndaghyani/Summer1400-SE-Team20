using System;
using SearchEngine.Classes;

namespace SearchConsoleApplication
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