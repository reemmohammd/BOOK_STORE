using BOOK_STORE.Models;
using BOOK_STORE.Models.Repositories;
using BOOK_STORE.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BOOK_STORE.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepository<Book> bookRepository,
            IBookStoreRepository<Author>authorRepository,
            IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string FileName = UploadFile(model.File) ?? string.Empty;
                 
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "please select an author from the list";
                        return View(GetAllAuthors());
                    }

                    var author = authorRepository.find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Titel = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl =FileName
                    };
                    bookRepository.add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "you have to fill all the requeried fields");
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Titel,
                Description = book.Description,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl

            };
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel viewModel)
        {
            try
            {
                // لحفظ مسار الصورة
                string FileName = UploadFile(viewModel.File,viewModel.ImageUrl);
             
                var author = authorRepository.find(viewModel.AuthorId);
                Book book = new Book
                {
                    Id = viewModel.BookId,
                    Titel = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImageUrl=FileName
                };
                bookRepository.update(viewModel.BookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.find(id);

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "...please select an author..." });
            return authors;
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return vmodel;
        }
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullpath = Path.Combine(uploads,file. FileName);
                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return file.FileName;
            }
            return null;
        }
        string UploadFile(IFormFile file, string ImageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string newpath = Path.Combine(uploads, file.FileName);
                //delete the old file
                string oldpath = Path.Combine(uploads, ImageUrl);
                if (oldpath != newpath)
                {
                    System.IO.File.Delete(oldpath);
                    // save the new file
                    file.CopyTo(new FileStream(newpath, FileMode.Create));
                }

                return file.FileName;

            }
            return ImageUrl;
        }
    }
}
