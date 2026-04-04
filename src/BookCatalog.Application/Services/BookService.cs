using AutoMapper;
using BookCatalog.Application.Dtos;
using BookCatalog.Application.Models.Response;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Repositories;


namespace BookCatalog.Application.Services
{
    public class BookService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public BookService(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public ApiResponse<List<BookDto>> GetAllBooks()
        {
            var books = _unitOfWork.BookRepository.GetAllBooks();
            var response = _mapper.Map<List<BookDto>>(books);

            return ApiResponse<List<BookDto>>.SuccessResponse(response);
        }

        public ApiResponse<List<BooksWithkeywords>> GetBookWithKeywords()
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
            return ApiResponse<List<BooksWithkeywords>>.SuccessResponse(booksWithKeywords);
        }

        
        public ApiResponse<BookDto> GetBooksById(int id)
        {
            var book = _unitOfWork.BookRepository.GetBookById(id);
            if (book == null)
            {
                return ApiResponse<BookDto>.FailureResponse("Book not found");
            }

            var result = _mapper.Map<BookDto>(book);

            return ApiResponse<BookDto>.SuccessResponse(result);
        }

        
        public ApiResponse<BookDto> CreateBook(BookDto bookRequest)
        {
            if (string.IsNullOrWhiteSpace(bookRequest.Title))
            {
                return ApiResponse<BookDto>.FailureResponse("Title is required.");
            }

            var book = _mapper.Map<Book>(bookRequest);
            _unitOfWork.BeginTransaction();
            _unitOfWork.BookRepository.AddBook(book);
            _unitOfWork.Complete();
            _unitOfWork.Commit();

            return ApiResponse<BookDto>.SuccessResponse(bookRequest);
        }

        
        public ApiResponse<BookDto> UpdateBook(int id, BookDto bookRequest)
        {
            var existingBook = _unitOfWork.BookRepository.GetBookById(id);
            if (existingBook == null)
            {
                return ApiResponse<BookDto>.FailureResponse("Book not found");
            }
            existingBook.Title = bookRequest.Title;
            existingBook.Author = bookRequest.Author;
            existingBook.PublicationDate = bookRequest.PublicationDate;
            existingBook.ISBN = bookRequest.ISBN;

            _unitOfWork.BookRepository.UpdateBook(existingBook);
            _unitOfWork.Complete();
            return ApiResponse<BookDto>.SuccessResponse(bookRequest);
        }

        
        public ApiResponse<BookDto> DeleteBook(int id)
        {
            var existing = _unitOfWork.BookRepository.GetBookById(id);
            if (existing == null)
            {
                return ApiResponse<BookDto>.FailureResponse("Book not found");
            }
            _unitOfWork.BookRepository.DeleteBook(existing);
            _unitOfWork.Complete();
            return ApiResponse<BookDto>.SuccessResponse(null);
        }

     
    }
}
