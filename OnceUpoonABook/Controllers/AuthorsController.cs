using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public IActionResult Index()
        {
            var allAuthors = authorService.GetAll().OrderBy(author => author.AuthorName);
            return View(allAuthors);
        }

        public IActionResult Add()
        {
            return View();
        }

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
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = authorService.GetById(id);
            if (actorDetails == null) return View("Error");
            return View(actorDetails);
        }

        public IActionResult Edit(int id)
        {
            var actorDetails = authorService.GetById(id);
            if (actorDetails == null) return View("Error");
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);

            }

            authorService.Update(id, author);
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int id)
        {
            var actorDetails = authorService.GetById(id);
            if (actorDetails == null) return View("Error");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id)
        {
            var actorDetails =  authorService.GetById(id);
            if (actorDetails == null) return View("Error");

            authorService.DeleteAuthor(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
