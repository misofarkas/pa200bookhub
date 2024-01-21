using BusinessLayer.DTOs.Review;
using BusinessLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models;
using WebMVC.Models.Review;

namespace WebMVC.Controllers
{
    [Route("reviews")]
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookService _bookService;
        private readonly ICustomerService _customerService;
        private readonly IMemoryCache _memoryCache;

        public ReviewController(IReviewService reviewService, IBookService bookService, ICustomerService customerService, IMemoryCache memoryCache)
        {
            _reviewService = reviewService;
            _bookService = bookService;
            _customerService = customerService;
            _memoryCache = memoryCache;
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create(int bookId)
        {
            try
            {
                var book = await _bookService.GetBookAsync(bookId);
                var viewModel = new BasicReviewViewModel
                {
                    BookTitle = book!.Title,
                    BookId = book.Id,
                    CustomerUsername = User.Identity.Name,
                };
                _memoryCache.Remove($"book-{bookId}");
                return View(viewModel);
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

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BasicReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = await _customerService.GetCustomerByEmailAsync(User.Identity.Name);
                    model.CustomerId = customer.Id;
                    await _reviewService.CreateReviewAsync(model.Adapt<ReviewCreateUpdateDTO>());
                    _memoryCache.Remove($"book-{model.BookId}");
                    return RedirectToAction("Index", "Book");
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
            return View(model);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, int bookId)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = $"No review with ID {id} was found.",
                };
                return View("Error", errorModel);
            }
            _memoryCache.Remove($"book-{bookId}");
            return RedirectToAction("Index", "Book");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id, int bookId)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = $"No review with ID {id} was found.",
                };
                return View("Error", errorModel);
            }
            var result = review.Adapt<BasicReviewViewModel>();
            result.BookId = bookId;
            _memoryCache.Remove($"book-{bookId}");
            return View(result);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BasicReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CustomerId = (await _customerService.GetCustomerByEmailAsync(User.Identity.Name))!.Id;
                    await _reviewService.UpdateReviewAsync(id, model.Adapt<ReviewCreateUpdateDTO>());
                    _memoryCache.Remove($"book-{model.BookId}");
                    return RedirectToAction("Index", "Book");
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
            return View(model);
        }

    }
}
