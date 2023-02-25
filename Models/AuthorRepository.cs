using BOOK_STORE.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BOOK_STORE.Models
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        IList<Author> Authors;
        public AuthorRepository()
        {
            Authors = new List<Author>()
            {
                new Author {Id=1 , FullName = "Anders Hejlsberg"},
                new Author {Id=2 , FullName = "Bjarne Stroustrup"},
                new Author {Id=3 , FullName = "Guido van Rossum"}
            };
        }

       

        public void add(Author entity)
        {
            entity.Id = Authors.Max(A => A.Id) +1; // to make AuthorId increase automatically .
            Authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author =find(id);
            Authors.Remove(author); 
        }

        public Author find(int id)
        {
            var author = Authors.SingleOrDefault(a => a.Id == id);
            return author;  
        }

        public IList<Author> List()
        {
            return Authors; 
        }

        

        public void update(int id, Author newAuthor )
        {
            var author = find(id);
            author.FullName = newAuthor.FullName;   
        }

        
    }
}
