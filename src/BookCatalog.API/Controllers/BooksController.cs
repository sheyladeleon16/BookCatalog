using AutoMapper;
using BookCatalog.Application.Dtos;
using BookCatalog.Application.Models.Response;
using BookCatalog.Application.Services;
using BookCatalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookCatalog.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService) 
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ApiResponse<List<BookDto>> GetBooks() =>
            _bookService.GetAllBooks();
            
        

        [HttpGet("with-keywords")]
        public ApiResponse<List<BooksWithkeywords>> GetAll() =>
            _bookService.GetBookWithKeywords();
       

        [HttpGet("{id}")]
        public ApiResponse<BookDto> GetById(int id)
        {
            return _bookService.GetBooksById(id);
        }

        [HttpPost]
        public ApiResponse<BookDto> Create(BookDto bookRequest)
        {
            return _bookService.CreateBook(bookRequest);
        }

        [HttpPut("{id}")]
        public ApiResponse<BookDto> Update(int id, BookDto bookRequest)
        {
            return _bookService.UpdateBook(id, bookRequest);
        }

        [HttpDelete("{id}")]
        public ApiResponse<BookDto> Delete(int id) =>
            _bookService.DeleteBook(id);
    }
}
