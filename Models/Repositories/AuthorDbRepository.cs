
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOK_STORE.Models.Repositories
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        BookstoreDbcontext db;
        public AuthorDbRepository(BookstoreDbcontext _db)
        {
           db = _db;
        }



        public void add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = find(id);
            db. Authors.Remove(author);
            db.SaveChanges();
        }

        public Author find(int id)
        {
            var author = db. Authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }



        public void update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();
        }


    }
}
