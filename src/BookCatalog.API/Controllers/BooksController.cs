using BookCatalog.API.Models;
using BookCatalog.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BookCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> _book = new List<Book>()
        {
                new Book{ Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublicationDate = new DateTime(1925, 4, 10), ISBN = "978-0743273565"},
                new Book{ Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublicationDate = new DateTime(1960, 7, 11), ISBN = "978-0061120084"},
                new Book{ Id = 3, Title = "1984", Author = "George Orwell", PublicationDate = new DateTime(1949, 6, 8), ISBN = "978-0451524935" }
        };

        [HttpGet]
        public IActionResult GetBooks()
        {
            var booksDtos = _book.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                ISBN = book.ISBN,
                
            }).ToList();
            return Ok(booksDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _book.FirstOrDefault(b => b.Id == id);
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
            int newId = _book.Any() ? _book.Max(b => b.Id) + 1 : 1;
            var book = new Book 
            {
                Id = newId,
                Title = bookRequest.Title,
                Author = bookRequest.Author,
                PublicationDate = bookRequest.PublicationDate,
                ISBN = bookRequest.ISBN,
            };

            _book.Add(book);
            return Ok(new {Id = book.Id});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDto bookRequest)
        {
            var existingBook = _book.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = bookRequest.Title;
            existingBook.Author = bookRequest.Author;
            existingBook.PublicationDate = bookRequest.PublicationDate;
            existingBook.ISBN = bookRequest.ISBN;
   
            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _book.FirstOrDefault(b => b.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            _book.Remove(existing);
            return (NoContent());
        }

    }
}
