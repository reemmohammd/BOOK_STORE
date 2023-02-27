using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BOOK_STORE.Models.Repositories
{
    public class BookDbRepository : IBookStoreRepository<Book>
    // we use : when implemtation
    {
        BookstoreDbcontext db;
        public BookDbRepository(BookstoreDbcontext _db)
        {
            db = _db;
        }
    
        public void add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.Author).ToList();
        }

        public void update(int id, Book newbook)
        {
            db.Update(newbook);
            db.SaveChanges();
        }
    }
}

