using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Publisher;
using BusinessLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Models.Author;
using WebMVC.Models.Publisher;

namespace WebMVC.Controllers
{
    [Route("publishers")]
    [Authorize]
    public class PublisherController : Controller
    {

        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            var viewModel = new PublisherListViewModel
            {
                Publishers = publishers.Adapt<IEnumerable<BasicPublisherViewModel>>(),
            };
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BasicPublisherViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _publisherService.CreatePublisherAsync(model.Adapt<PublisherCreateUpdateDTO>());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    var ErrorModel = new ErrorViewModel
                    {
                        RequestId = e.Message,
                    };
                    return View("Error", ErrorModel);
                }

            }
            var errorModel = new ErrorViewModel
            {
                RequestId = "Invalid model state",
            };
            return View("Error", errorModel);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var publisher = await _publisherService.GetPublisherAsync(id);
            if (publisher == null)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = $"No publisher with ID {id} was found.",
                };
                return View("Error", errorModel);
            }
            return View(publisher.Adapt<BasicPublisherViewModel>());
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BasicPublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = "Invalid model state",
                };
                return View("Error", errorModel);
            }
            try
            {
                var result = await _publisherService.UpdatePublisherAsync(id, model.Adapt<PublisherCreateUpdateDTO>());
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                var ErrorModel = new ErrorViewModel
                {
                    RequestId = e.Message,
                };
                return View("Error", ErrorModel);
            }
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _publisherService.DeletePublisherAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                var ErrorModel = new ErrorViewModel
                {
                    RequestId = e.Message,
                };
                return View("Error", ErrorModel);
            }
        }
    }
}
