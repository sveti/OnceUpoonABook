using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services.Authors;
using OnceUpoonABook.Data.Services.Books;
using OnceUpoonABook.Data.Services.Publishers;
using OnceUpoonABook.Data.ViewModels;

namespace OnceUpoonABook.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IPublisherService publisherService;
        public BooksController(IBookService bookService,
            IPublisherService publisherService,
            IAuthorService authorService)
        {
            this.bookService = bookService;
            this.publisherService = publisherService;
            this.authorService = authorService;
        }

        [Authorize(Roles = "Admin,Member")]
        public IActionResult Index()
        {
            var allBooks = bookService.GetAllBooks();
            return View(allBooks);
        }
        [Authorize(Roles = "Admin,Member")]
        public IActionResult Details(int id)
        {
            var book = bookService.GetBookById(id);
            return View(book);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var publishers = publisherService.GetAll().OrderBy(publisher => publisher.Name);
            var authors = authorService.GetAll().OrderBy(author => author.AuthorName);

            ViewBag.Authors = authors;
            ViewBag.Publishers = publishers;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(AddBookViewModel addBookViewModel)
        {

            if(addBookViewModel.newAuthor != null && !String.IsNullOrWhiteSpace(addBookViewModel.newAuthor.AuthorName)) {

                if (!ModelState.IsValid)
                {
                    var author = authorService.Add(addBookViewModel.newAuthor);
                    addBookViewModel.AuthorIds.Add(author.Id);
                }

            }
            if (!ModelState.IsValid)
            {
                bookService.Add(addBookViewModel);
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var publishers = publisherService.GetAll().OrderBy(publisher => publisher.Name);
            var authors = authorService.GetAll().OrderBy(author => author.AuthorName);

            ViewBag.Authors = authors;
            ViewBag.Publishers = publishers;

            var book = bookService.GetBookById(id);
            EditBookViewModel editBookViewModel = new EditBookViewModel();
            editBookViewModel.Book = book;  
            return View(editBookViewModel);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, EditBookViewModel bookViewModel)
        {
            bookViewModel.Book.Id = id;
            bookService.UpdateBook(bookViewModel);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {

            var bookdetails = bookService.GetBookById(id);
            if (bookdetails == null) return View("Error");

            var publishers = publisherService.GetAll().OrderBy(publisher => publisher.Name);
            var authors = authorService.GetAll().OrderBy(author => author.AuthorName);

            ViewBag.Authors = authors;
            ViewBag.Publishers = publishers;
            EditBookViewModel editBookViewModel = new EditBookViewModel();
            editBookViewModel.Book = bookdetails;
            return View(editBookViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmation(int id)
        {
            var bookdetails = bookService.GetBookById(id);
            if (bookdetails == null) return View("Error");

            bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
