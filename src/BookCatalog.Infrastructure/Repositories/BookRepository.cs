using BookCatalog.Domain.Entities;
using BookCatalog.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Infrastructure.Repositories
{
    public class BookRepository
    {
        private readonly ApplicationContext _context;

        public BookRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Book? GetBookById(int Id)
        {
            return _context.Books.Find(Id);
        }
        
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
        public List<Book> GetBooksWithKeywords()
        {
           return _context.Books.Include(b => b.Keyword).ToList();

        }
        public Book AddBook(Book book)
        {
            _context.Books.Add(book);
            //_context.SaveChanges();
            return book;
        }
        public void UpdateBook(Book book)
        {
            _context.Books.Add(book);
            //_context.SaveChanges();

        }
        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            //_context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
