using System;
using System.Collections.Generic;

namespace SearchEngine.Classes.IO.Database.Models
{
    public class Document
    {
        public int Id { get; set; }
        public int DocumentNumber { get; set; }
        
        public List<Word_Document> WordDocuments { get; set; }
        
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }

            Document doc = (Document) obj;
            return (DocumentNumber == doc.DocumentNumber);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}