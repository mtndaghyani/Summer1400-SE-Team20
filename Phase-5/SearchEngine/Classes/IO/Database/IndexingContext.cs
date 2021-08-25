using Microsoft.EntityFrameworkCore;
using SearchEngine.Classes.IO.Database.Models;

namespace SearchEngine.Classes.IO.Database
{
    public class IndexingContext:DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<WordDocument> WordDocuments { get; set; }
        
        public IndexingContext(DbContextOptions<IndexingContext> contextOptions) : base(contextOptions) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordDocument>()
                .HasOne(x => x.Document)
                .WithMany(x => x.WordDocuments)
                .HasForeignKey(x => x.DocumentId);
            
            modelBuilder.Entity<WordDocument>()
                .HasOne(x => x.Word)
                .WithMany(x => x.WordDocuments)
                .HasForeignKey(x => x.WordId);
        }
    }
}