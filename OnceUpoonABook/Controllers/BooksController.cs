using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;
using OnceUpoonABook.Data.ViewModels;

namespace OnceUpoonABook.Controllers
{
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

        public IActionResult Index()
        {
            var allBooks = bookService.GetAllBooks();
            return View(allBooks);
        }

        public IActionResult Details(int id)
        {
            var book = bookService.GetBookById(id);
            return View(book);
        }

        public IActionResult Add()
        {
            var publishers = publisherService.GetAll().OrderBy(publisher => publisher.Name);
            var authors = authorService.GetAll().OrderBy(author => author.AuthorName);

            ViewBag.Authors = authors;
            ViewBag.Publishers = publishers;

            return View();
        }

        [HttpPost]
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
        public IActionResult Edit(int id, EditBookViewModel bookViewModel)
        {
            bookViewModel.Book.Id = id;
            bookService.UpdateBook(bookViewModel);

            return RedirectToAction(nameof(Index));
        }


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
        public IActionResult DeleteConfirmation(int id)
        {
            var bookdetails = bookService.GetBookById(id);
            if (bookdetails == null) return View("Error");

            bookService.DeleteBook(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
