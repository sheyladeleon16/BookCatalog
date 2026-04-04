using BookCatalog.Domain.Entities;
using BookCatalog.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Infrastructure.Repositories
{
    public class KeywordRepository
    {
        private readonly ApplicationContext _context;

        public KeywordRepository(ApplicationContext context)
        {
            _context = context;
        }

        public Keyword? GetKeywordById(int Id)
        {
            return _context.Keywords.Find(Id);
        }

        public List<Keyword> GetAllKeywords()
        {
            return _context.Keywords.ToList();
        }
        public Keyword AddKeyword(Keyword book)
        {
            _context.Keywords.Add(book);
            return book;
        }
        public void UpdateKeyword(Keyword book)
        {
            _context.Keywords.Add(book);
        }
        public void DeleteKeyword(Keyword book)
        {
            _context.Keywords.Remove(book); 
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }   
    }
}
