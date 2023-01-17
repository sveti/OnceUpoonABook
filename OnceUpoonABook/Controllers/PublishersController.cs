using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services.Publishers;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Controllers
{
    [Authorize]
    public class PublishersController : Controller
	{

        private readonly IPublisherService publisherService;

		public PublishersController(IPublisherService publisherService)
		{
			this.publisherService = publisherService;
		}
        [Authorize(Roles = "Admin,Member")]
        public IActionResult Index()
        {
            var allPublishers = publisherService.GetAll().OrderBy(publisher => publisher.Name);
            return View(allPublishers);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Add([Bind("Name,YearCreated,LogoURL")] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);

            }

            publisherService.Add(publisher);
            return RedirectToAction(nameof(Index));


        }
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Details(int id)
        {
            var publisherDetails = publisherService.GetById(id);
            if (publisherDetails == null) return View("Error");
            return View(publisherDetails);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var publisherDetails = publisherService.GetById(id);
            if (publisherDetails == null) return View("Error");
            return View(publisherDetails);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);

            }

            publisherService.Update(id, publisher);
            return RedirectToAction(nameof(Index));


        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var publisher = publisherService.GetById(id);
            if (publisher == null) return View("Error");
            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmation(int id)
        {
            var publisher = publisherService.GetById(id);
            if (publisher == null) return View("Error");

            publisherService.DeletePublisher(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
