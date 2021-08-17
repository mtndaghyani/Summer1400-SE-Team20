using Microsoft.EntityFrameworkCore;
using SearchEngine.Classes.IO.Database;

namespace SearchEngineTests.DatabaseTests
{
    public class DatabaseInvertedIndexTestsInMemory : DatabaseInvertedIndexTests
    {
        public DatabaseInvertedIndexTestsInMemory() :
                        base(new DbContextOptionsBuilder<IndexingContext>()
                        .UseInMemoryDatabase("MyInMemoryDB").Options){}
    }
}