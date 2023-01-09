using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnceUpoonABook.Data;
using OnceUpoonABook.Data.Services;
using OnceUpoonABook.Models;

namespace OnceUpoonABook.Controllers
{
	public class PublishersController : Controller
	{

        private readonly IPublisherService publisherService;

		public PublishersController(IPublisherService publisherService)
		{
			this.publisherService = publisherService;
		}

        public IActionResult Index()
        {
            var allPublishers = publisherService.GetAll().OrderBy(publisher => publisher.Name);
            return View(allPublishers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,YearCreated,LogoURL")] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);

            }

            publisherService.Add(publisher);
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Details(int id)
        {
            var publisherDetails = publisherService.GetById(id);
            if (publisherDetails == null) return View("Error");
            return View(publisherDetails);
        }

        public IActionResult Edit(int id)
        {
            var publisherDetails = publisherService.GetById(id);
            if (publisherDetails == null) return View("Error");
            return View(publisherDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return View(publisher);

            }

            publisherService.Update(id, publisher);
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int id)
        {
            var publisher = publisherService.GetById(id);
            if (publisher == null) return View("Error");
            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmation(int id)
        {
            var publisher = publisherService.GetById(id);
            if (publisher == null) return View("Error");

            publisherService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
