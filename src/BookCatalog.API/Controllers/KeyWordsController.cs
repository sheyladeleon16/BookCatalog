using BookCatalog.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BookCatalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyWordsController: ControllerBase
    {
        private static readonly List<Keyword> _keyword = new List<Keyword>()
        {
                new Keyword{ Id = 1, Tag = "Historical" },
                new Keyword{ Id = 2, Tag = "Tecnology" },
                new Keyword{ Id = 3, Tag = "Psicology" },
                new Keyword{ Id = 4, Tag = "Filosofy" }
        };

        [HttpGet]
        public ActionResult<List<Keyword>> GetKeyword()
        {
            return(_keyword);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var keyword = _keyword.FirstOrDefault(k => k.Id == id);
            if (keyword == null)
            {
                return NotFound();
            }
            return Ok(keyword);
        }

        [HttpPost]
        public IActionResult Create(Keyword keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword.Tag))
            {
                return BadRequest("Tag is required.");
            }
            int newId = _keyword.Any() ? _keyword.Max(k => k.Id) + 1 : 1;
            keyword.Id = newId;

            _keyword.Add(keyword);
            return Ok(_keyword);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Keyword keyword)
        {
            var existingkeyword = _keyword.FirstOrDefault(k => k.Id == id);
            if (existingkeyword == null)
            {
                return NotFound();
            }
            existingkeyword.Tag = keyword.Tag;

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
