using AutoMapper;
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
        private readonly IMapper _mapper;   
        public BooksController(ApplicationContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;

        }
        
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books.ToList();
            var response = _mapper.Map<List<BookDto>>(books);
           
            return Ok(response);
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
       
            var result = _mapper.Map<BookDto>(book);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(BookDto bookRequest)
        {
            if (string.IsNullOrWhiteSpace(bookRequest.Title))
            {
                return BadRequest("Title is required.");
            }
       
            var book = _mapper.Map<Book>(bookRequest);
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
