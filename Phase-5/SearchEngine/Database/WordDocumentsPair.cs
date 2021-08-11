using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SearchEngine.Database
{
    public class WordDocumentsPair
    {
        [Key] 
        public string Word { get; set; }
        public List<Document> Documents { get; set; }
    }
    
    

}