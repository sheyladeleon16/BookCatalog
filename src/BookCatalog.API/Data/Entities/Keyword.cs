using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Data.Entities
{
    public class Keyword
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Tag { get; set; } = string.Empty;
        public int BookId { get; set; }

    }
}
