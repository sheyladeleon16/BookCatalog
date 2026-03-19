using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [StringLength(200)]
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        
    }
}
