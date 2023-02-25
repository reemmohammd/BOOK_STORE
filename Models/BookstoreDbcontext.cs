using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOOK_STORE.Models
{
    public class BookstoreDbcontext:DbContext
    {
        public BookstoreDbcontext(DbContextOptions<BookstoreDbcontext>options):base(options)
        {

        }
        public DbSet<Author>Authors  { get; set; } // to make a table
        public DbSet<Book> Books { get; set; } 

    }
}
