using BookCatalog.API.Data.Entities;
using BookCatalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BookCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyWordsController: ControllerBase
    {
        private static readonly List<Keyword> _keyword = new List<Keyword>()
        {
                new Keyword{ Id = 1, Words = "Historical"},
                new Keyword{ Id = 2, Words = "Tecnology" },
                new Keyword{ Id = 3, Words = "Psicology" },
                new Keyword{ Id = 4, Words = "Filosofy"}
        };

        [HttpGet]
        public IActionResult GetKeyword()
        {
            var keywordsDtos = _keyword.Select(keyword => new KeywordDto
            {
                Id = keyword.Id,
                Words = keyword.Words


            }).ToList();
            return Ok(keywordsDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var keyword = _keyword.FirstOrDefault(k => k.Id == id);
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
                return BadRequest("Key is required.");
            }
            int newId = _keyword.Any() ? _keyword.Max(k => k.Id) + 1 : 1;
            var keyword = new Keyword
            {
                Id = newId,
                Words = keywordRequest.Words,
            };
            _keyword.Add(keyword);
            return Ok(new { Id = keyword.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, KeywordDto keywordRequest)
        {
            var existingkeyword = _keyword.FirstOrDefault(k => k.Id == id);
            if (existingkeyword == null)
            {
                return NotFound();
            }
            existingkeyword.Words = keywordRequest.Words;

            return (NoContent());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _keyword.FirstOrDefault(b => b.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            _keyword.Remove(existing);
            return (NoContent());
        }

    }
}
