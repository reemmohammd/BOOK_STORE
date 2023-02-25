using BOOK_STORE.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOOK_STORE.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Requierd]
        [MaxLength(30)]
        [MinLength(5)]
        public string Title { get; set; }

        [Requierd]
        [StringLength(120,MinimumLength =5)]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }

    internal class RequierdAttribute : Attribute
    {
    }
}
