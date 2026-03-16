using BookCatalog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<Book> Books { get; set; }
       public DbSet<Keyword> Keywords { get; set; }
    }
}
