using BookCatalog.Domain.Entities;
using BookCatalog.Persistence.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Keyword> Keywords { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration<Book>(new BookConfiguration());
            }
        }
    }
}
