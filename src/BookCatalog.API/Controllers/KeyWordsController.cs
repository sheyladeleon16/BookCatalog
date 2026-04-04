
using BookCatalog.API.Models;
using BookCatalog.Domain.Entities;
using BookCatalog.Infrastructure.Repositories;
using BookCatalog.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyWordsController: ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public KeyWordsController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetKeyword()
        {
            var keywordsDtos = _unitOfWork.KeywordRepository.GetAllKeywords();
            var result = keywordsDtos.Select(k => new KeywordDto
            {
                Id = k.Id,
                Words = k.Words,
                BookId = k.BookId
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var keyword = _unitOfWork.KeywordRepository.GetKeywordById(id);
            if (keyword == null)
            {
                return NotFound();
            }
            var result = new KeywordDto
            {
                Id = keyword.Id,
                Words = keyword.Words

            };
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(KeywordDto keywordRequest)
        {
            if (string.IsNullOrWhiteSpace(keywordRequest.Words))
            {
                return BadRequest("Word is required.");
            }
            
            var keyword = new Keyword
            {
                Words = keywordRequest.Words,
                BookId = keywordRequest.BookId
            };
            _unitOfWork.BeginTransaction();
            _unitOfWork.KeywordRepository.AddKeyword(keyword); 
            _unitOfWork.Complete();
            _unitOfWork.Commit();   

            return Ok(new { Id = keyword.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, KeywordDto keywordRequest)
        {
            var existingkeyword = _unitOfWork.KeywordRepository.GetKeywordById(id);
            if (existingkeyword == null)
            {
                return NotFound();
            }
            existingkeyword.Words = keywordRequest.Words;
            _unitOfWork.KeywordRepository.UpdateKeyword(existingkeyword);   
            _unitOfWork.Complete();
            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.KeywordRepository.GetKeywordById(id);
            if (existing == null)
            {
                return NotFound();
            }
            _unitOfWork.KeywordRepository.DeleteKeyword(existing);
            _unitOfWork.Complete(); 
            return (NoContent());
        }

    }
}
