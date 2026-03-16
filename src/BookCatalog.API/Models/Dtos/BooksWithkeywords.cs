using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models.Dtos
{
    public class BooksWithkeywords
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [StringLength(150)]
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;
        public List<KeywordDto> Keywords { get; set; } = new();

    }
}
