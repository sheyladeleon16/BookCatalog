using BookCatalog.Domain.Entities;
using BookCatalog.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Infrastructure.Repositories
{
    public class UnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly BookRepository _bookReposiroty;
        private readonly KeywordRepository _keywordRepository;

        public UnitOfWork(ApplicationContext context, BookRepository bookRepository, KeywordRepository keywordRepository)
        {
            _context = context;
            _bookReposiroty = bookRepository;
            _keywordRepository = keywordRepository;
        }
        public BookRepository BookRepository => _bookReposiroty;
        public KeywordRepository KeywordRepository => _keywordRepository;
        public void Complete()
        {
            _context.SaveChanges();
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _context.Database.CommitTransaction();
        }
        public void Rollback()
        {
            _context.Database.RollbackTransaction();

        }
    }
}
