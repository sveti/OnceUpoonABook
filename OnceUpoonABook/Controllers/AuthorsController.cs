using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
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
            var actorDetails = authorService.GetById(id);
            if (actorDetails == null) return View("Error");
            return View(actorDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var actorDetails = authorService.GetById(id);
            if (actorDetails == null) return View("Error");
            return View(actorDetails);
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
            var actorDetails = authorService.GetById(id);
            if (actorDetails == null) return View("Error");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmation(int id)
        {
            var actorDetails =  authorService.GetById(id);
            if (actorDetails == null) return View("Error");

            authorService.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
