using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchEngine.Database
{
    public class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentId { get; set; }
    }
}