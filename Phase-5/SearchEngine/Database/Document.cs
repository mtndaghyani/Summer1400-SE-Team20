using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using SearchEngine.Interfaces;

namespace SearchEngine.Database
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentIdentification { get; set; }

        public List<Word> Words { get; set; }
        
        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || this.GetType() != obj.GetType())
            {
                return false;
            }

            Document doc = (Document) obj;
            return (DocumentIdentification == doc.DocumentIdentification);
        }

        public override int GetHashCode()
        {
            return DocumentIdentification;
        }
    }
}