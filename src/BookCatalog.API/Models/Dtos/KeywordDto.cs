using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models
{
    public class KeywordDto
    {
        public int Id { get; set; }
        public string Words { get; set; } = string.Empty;
        public int BookId { get; set; }
    }
}
