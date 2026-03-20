using BookCatalog.API.Data;
using BookCatalog.API.Data.Entities;
using BookCatalog.API.Models;
using BookCatalog.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Identity.Client;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public BooksController(ApplicationContext context) 
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            var booksDtos = _context.Books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                ISBN = book.ISBN,
                
            }).ToList();
            return Ok(booksDtos);
        }

        [HttpGet("with-keywords")]
        public IActionResult GetAll()
        {
            var booksWithKeywords = _context.Books
            .Select(b => new BooksWithkeywords()
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                PublicationDate = b.PublicationDate,
                Keywords = b.Keyword.Select(k => new KeywordDto
                {
                    Id = k.Id,
                    Words = k.Words
                }).ToList()
            }).ToList();
            return Ok(booksWithKeywords);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            var result = new BookDto
            {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublicationDate = book.PublicationDate,
                    ISBN = book.ISBN,
                    
            };
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(BookDto bookRequest)
        {
            if (string.IsNullOrWhiteSpace(bookRequest.Title))
            {
                return BadRequest("Title is required.");
            }
         
            var book = new Book 
            {
                Title = bookRequest.Title,
                Author = bookRequest.Author,
                PublicationDate = bookRequest.PublicationDate,
                ISBN = bookRequest.ISBN,
            };

            _context.Add(book);
            _context.SaveChanges();

            return Ok(new {Id = book.Id});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDto bookRequest)
        {
            var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = bookRequest.Title;
            existingBook.Author = bookRequest.Author;
            existingBook.PublicationDate = bookRequest.PublicationDate;
            existingBook.ISBN = bookRequest.ISBN;

            _context.Update(existingBook);
            _context.SaveChanges();
   
            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _context.Books.FirstOrDefault(b => b.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            _context.Remove(existing);
            _context.SaveChanges();
            return (NoContent());
        }

    }
}
