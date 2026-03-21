using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Domain.Entities
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Words { get; set; } = string.Empty;
        public int BookId { get; set; }
    }
}
