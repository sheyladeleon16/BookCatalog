using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.Dtos
{
    public class KeywordDto
    {
        public int Id { get; set; }
        public string Words { get; set; } = string.Empty;
        public int BookId { get; set; }
    }
}
