using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        public IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookstoreRepository<Book> bookRepository, IBookstoreRepository<Author> authorRepository,
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
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View(GetAllAuthors());
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
                    string filename = string.Empty;
                    if(model.File != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath, "upload");
                        filename = model.File.FileName;
                        string fullpath = Path.Combine(uploads, filename);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }

                    if (model.AuthorId == -1)
                    {
                        ViewBag.message = "Please Select an Author!";

                        return View(GetAllAuthors());
                    }

                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        ImgUrl = filename,
                        Author = authorRepository.Find(model.AuthorId)
                    };

                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

           
                ModelState.AddModelError("", "you have to fill all required fields");
                return View(GetAllAuthors());
            
           
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorid = book.Author == null ? 0: book.Author.Id;
            var ViewModle = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorid,
                Authors = authorRepository.List().ToList(),
                ImgUrl = book.ImgUrl
                
            };
            return View(ViewModle);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {  
            try
            {
                string filename = string.Empty;
                if (viewModel.File != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "upload");
                    filename = viewModel.File.FileName;
                    string fullpath = Path.Combine(uploads, filename);
                    
                    
                    string oldfilename = viewModel.ImgUrl;

                    string fullOldPath = Path.Combine(uploads, oldfilename);

                    if (fullpath != fullOldPath)
                    {
                        System.IO.File.Delete(fullOldPath);
                        viewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }

                }
                var author = authorRepository.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Id=viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImgUrl = filename
                };

                bookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
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

        List<Author> fillselectlist()
        {
            var autthors = authorRepository.List().ToList();
            autthors.Insert(0, new Author { Id= -1, FullName="--- Pleaze Select an Author ---"});
            return(autthors);
        }

        BookAuthorViewModel GetAllAuthors()
        {
            var model1 = new BookAuthorViewModel
            {
                Authors = fillselectlist()
            };
            return(model1);
        }
       
    }
}
