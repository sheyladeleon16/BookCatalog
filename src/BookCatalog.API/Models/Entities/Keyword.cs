using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models.Entities
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Tag { get; set; }                                                                                                                                                            
        public int BookId { get; set; }
       
    }
}
