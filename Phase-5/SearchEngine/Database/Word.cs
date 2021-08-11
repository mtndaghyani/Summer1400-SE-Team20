using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SearchEngine.Database
{
    public class Word
    {
        [Key]
        public int Id { get; set; }
        // [Key] 
        public string Statement { get; set; }
        public List<Document> Documents { get; set; }
    }
    
    

}