using SearchEngine.Classes.IO.Database;

namespace SearchEngineTests.Database
{
    public class DatabaseContextTests
    {

        public DatabaseContextTests()
        {
            Seed();
        }

        private void Seed()
        {
            using var context = new IndexingContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //TODO
        }
    }
}