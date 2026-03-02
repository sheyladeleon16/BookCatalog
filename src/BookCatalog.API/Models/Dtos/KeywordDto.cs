namespace BookCatalog.API.Models
{
    public class KeywordDto
    {
        public int Id { get; set; }                                                                                                     
        public string Tag { get; set; } = string.Empty;                                                                                                                                                             
        public int BookId { get; set; }
    }
}
