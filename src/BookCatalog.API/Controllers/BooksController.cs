using BookCatalog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> _book = new List<Book>()
        {
                new Book{ Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublicationDate = new DateTime(1925, 4, 10), ISBN = "978-0743273565" },
                new Book{ Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublicationDate = new DateTime(1960, 7, 11), ISBN = "978-0061120084" },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", PublicationDate = new DateTime(1949, 6, 8), ISBN = "978-0451524935" }
        };

        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return (_book);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var book = _book.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                return BadRequest("Title is required.");
            }
            int newId = _book.Any() ? _book.Max(b => b.Id) + 1 : 1;
            book.Id = newId;

            _book.Add(book);
            return Ok(_book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            var existingBook = _book.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublicationDate = book.PublicationDate;
            existingBook.ISBN = book.ISBN;
            existingBook.Updated = DateTime.UtcNow;

            return Ok(_book);
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
            return Ok(_book);
        }

    }
}
