using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace BOOK_STORE.Models.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
        // we use : when implemtation
    {
        List<Book> books; // Data source .. in future we will use EFCORE as a data source
        public BookRepository() // constructor
        {
            books = new List<Book>()
            {
                new Book()
                {
                    Id= 1, 
                    Titel = "C programming" ,
                    Description ="Microsoft introduced C " ,
                    ImageUrl = "C.jpg",
                    Author = new Author{Id =2}
                },
                new Book()
                {
                    Id= 2,
                    Titel = "Dart programming" ,
                    Description =" is a high-level general-purpose",
                    ImageUrl = "Dart.jpg",
                    Author = new Author()
                },
                new Book()
                {
                    Id= 3,
                    Titel = "python programming" ,
                    Description ="ABC programming language",
                    ImageUrl = "python.jpg",
                    Author = new Author()
                }
            };
        }
        public void add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1; // to make bookId increase automatically .
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = find(id);
            books.Remove(book);
        }

        public Book find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void update(int id,Book newbook)
        {
            var book = find(id);
            book.Titel = newbook.Titel;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImageUrl = newbook.ImageUrl;
        }
    }
}
