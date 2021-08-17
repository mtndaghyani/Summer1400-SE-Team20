using Microsoft.EntityFrameworkCore;
using SearchEngine.Classes.IO.Database.Models;

namespace SearchEngine.Classes.IO.Database
{
    public class IndexingContext:DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Word_Document> WordDocuments { get; set; }
        
        public IndexingContext(DbContextOptions<IndexingContext> contextOptions) : base(contextOptions) {}
        
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     base.OnConfiguring(optionsBuilder);
        //     optionsBuilder.UseSqlServer(ConnectionString);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word_Document>()
                .HasOne(x => x.Document)
                .WithMany(x => x.WordDocuments)
                .HasForeignKey(x => x.DocumentId);
            
            modelBuilder.Entity<Word_Document>()
                .HasOne(x => x.Word)
                .WithMany(x => x.WordDocuments)
                .HasForeignKey(x => x.WordId);
        }
    }
}