using AutoMapper;
using BookCatalog.API.Models;
using BookCatalog.API.Models.Dtos;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Repositories;
using BookCatalog.Persistence;
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
        //private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public BooksController(ApplicationContext context, IMapper mapper, UnitOfWork unitOfWork) 
        {
            //_context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _unitOfWork.BookRepository.GetAllBooks();
            var response = _mapper.Map<List<BookDto>>(books);
           
            return Ok(response);
        }

        [HttpGet("with-keywords")]
        public IActionResult GetAll()
        {
            var booksWithKeywords = _unitOfWork.BookRepository.GetBooksWithKeywords()
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
            var book = _unitOfWork.BookRepository.GetBookById(id);
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
            _unitOfWork.BeginTransaction();
            _unitOfWork.BookRepository.AddBook(book);
            _unitOfWork.Complete();
            _unitOfWork.Commit();

            return Ok(new {Id = book.Id});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BookDto bookRequest)
        {
            var existingBook = _unitOfWork.BookRepository.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = bookRequest.Title;
            existingBook.Author = bookRequest.Author;
            existingBook.PublicationDate = bookRequest.PublicationDate;
            existingBook.ISBN = bookRequest.ISBN;

            _unitOfWork.BookRepository.UpdateBook(existingBook);
            _unitOfWork.Complete();
            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.BookRepository.GetBookById(id);
            if (existing == null)
            {
                return NotFound();
            }
            _unitOfWork.BookRepository.DeleteBook(existing);
            _unitOfWork.Complete();
            return (NoContent());
        }

    }
}
