
using BookCatalog.API.Models;
using BookCatalog.Domain.Entities;
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
        private readonly ApplicationContext _Context;

        public KeyWordsController(ApplicationContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public IActionResult GetKeyword()
        {
            var keywordsDtos = _Context.Keywords.Select(keyword => new KeywordDto
            {
                Id = keyword.Id,
                Words = keyword.Words,
          

            }).ToList();
            return Ok(keywordsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var keyword = _Context.Keywords.FirstOrDefault(k => k.Id == id);
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
            _Context.Add(keyword);
            _Context.SaveChanges(); 

            return Ok(new { Id = keyword.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, KeywordDto keywordRequest)
        {
            var existingkeyword = _Context.Keywords.FirstOrDefault(k => k.Id == id);
            if (existingkeyword == null)
            {
                return NotFound();
            }
            existingkeyword.Words = keywordRequest.Words;
            _Context.Add(existingkeyword);
            _Context.SaveChanges();

            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _Context.Keywords.FirstOrDefault(b => b.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            _Context.Remove(existing);
            _Context.SaveChanges();
            return (NoContent());
        }

    }
}
