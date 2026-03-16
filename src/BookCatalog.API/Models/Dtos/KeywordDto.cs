using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models
{
    public class KeywordDto
    {
        public int Id { get; set; }                                                                                                     
        public string Tag { get; set; } = string.Empty;
        [StringLength(100)]
        public int BookId { get; set; }
    }
}
