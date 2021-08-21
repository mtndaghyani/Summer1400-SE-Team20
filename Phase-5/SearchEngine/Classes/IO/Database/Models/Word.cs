using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SearchEngine.Classes.IO.Database.Models
{
    public class Word
    {
        [Key] 
        public string Statement { get; set; }

        public List<WordDocument> WordDocuments { get; set; }
    }
}