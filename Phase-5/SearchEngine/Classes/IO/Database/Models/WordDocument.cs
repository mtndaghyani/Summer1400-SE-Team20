namespace SearchEngine.Classes.IO.Database.Models
{
    public class WordDocument
    {
        public int Id { get; set; }

        public string WordId { get; set; }
        public Word Word { get; set; }
        
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        
    }
}