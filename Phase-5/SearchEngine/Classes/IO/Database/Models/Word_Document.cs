namespace SearchEngine.Classes.IO.Database.Models
{
    public class Word_Document
    {
        public int Id { get; set; }

        public string WordId { get; set; }
        public Word Word { get; set; }
        
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        
    }
}