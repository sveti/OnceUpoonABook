using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services.Authors;
using OnceUpoonABook.Data.Services.Books;
using OnceUpoonABook.Data.ViewModels;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly IBookService bookService;

        public AuthorsController(IAuthorService authorService, IBookService bookService)
        {
            this.authorService = authorService;
            this.bookService = bookService;
        }
        [Authorize(Roles = "Admin,Member")]
        public IActionResult Index()
        {
            var allAuthors = authorService.GetAll().OrderBy(author => author.AuthorName);
            return View(allAuthors);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add([Bind("AuthorProfilePicURL,AuthorDescription,AuthorName")] Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);

            }

            authorService.Add(author);
            return RedirectToAction(nameof(Index));


        }

        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = authorService.GetById(id);
            if (authorDetails == null) return View("Error");

            var displayAuthor = new AuthorDetailsViewModel(authorDetails);
            displayAuthor.Books = bookService.GetBooksByAuthorId(id).ToList();

            return View(displayAuthor);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var authorDetails = authorService.GetById(id);
            if (authorDetails == null) return View("Error");
            return View(authorDetails);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);

            }

            authorService.Update(id, author);
            return RedirectToAction(nameof(Index));


        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var authorDetails = authorService.GetById(id);
            if (authorDetails == null) return View("Error");
            return View(authorDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmation(int id)
        {
            var authorDetails =  authorService.GetById(id);
            if (authorDetails == null) return View("Error");

            authorService.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
