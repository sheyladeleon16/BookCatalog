using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCatalog.API.Models.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [StringLength(150)]
        public string Author { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        [StringLength(20)]
        public string ISBN { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow; 

    }
}
