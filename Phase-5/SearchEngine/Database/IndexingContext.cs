using Microsoft.EntityFrameworkCore;

namespace SearchEngine.Database
{
    public class IndexingContext:DbContext
    {
        public DbSet<Word> WordDocumentsPairs { get; set; }
        public DbSet<Document> FileDocuments { get; set; }
        private const string ConnectionString = "Server=.; Database=IndexingDB; Trusted_Connection=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}