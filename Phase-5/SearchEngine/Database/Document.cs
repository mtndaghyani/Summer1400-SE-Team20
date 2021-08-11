using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using SearchEngine.Interfaces;

namespace SearchEngine.Database
{
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentId { get; set; }
        
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }

            Document doc = (Document) obj;
            return (DocumentId == doc.DocumentId);
        }

        public override int GetHashCode()
        {
            return DocumentId;
        }
    }
}