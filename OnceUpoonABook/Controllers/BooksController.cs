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
    }

}
