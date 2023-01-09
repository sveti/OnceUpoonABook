using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;

namespace OnceUpoonABook.Controllers
{
	public class BooksController : Controller
	{
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            var allBooks = bookService.GetAllBooks();
            return View(allBooks);
        }
    }
}
