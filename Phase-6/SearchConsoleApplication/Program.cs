using System;
using SearchEngine.Classes;

namespace SearchConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager("../../../Config.json");
            manager.Run();
        }
    }
}