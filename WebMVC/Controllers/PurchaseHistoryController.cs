using BusinessLayer.DTOs.PurchaseHistory;
using BusinessLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Models.PurchaseHistory;

namespace WebMVC.Controllers
{
    [Route("PurchaseHistory")]
    [Authorize]
    public class PurchaseHistoryController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICustomerService _customerService;
        private readonly IPurchaseHistoryService _purchaseHistoryService;
        public PurchaseHistoryController(IBookService bookService, ICustomerService customerService, IPurchaseHistoryService purchaseHistoryService)
        {
            _bookService = bookService;
            _customerService = customerService;
            _purchaseHistoryService = purchaseHistoryService;
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int bookId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return NotFound("No user with this email was found.");
            }
            var customer = await _customerService.GetCustomerByEmailAsync(userEmail);
            if (customer == null)
            {
                return NotFound("No customer with this email was found.");
            }
            var newPurchaseHistory = new PurchaseHistoryCreateUpdateDTO
            {
                BookId = bookId,
                CustomerId = customer.Id,
                PurchaseDate = DateTime.Now,
            };
            await _purchaseHistoryService.CreatePurchaseHistoryAsync(newPurchaseHistory);
            return View("PurchaseSuccess");
        }

        [HttpGet("book")]
        public async Task<ActionResult> BookPurchaseHistory(int bookId)
        {
            var purchaseHistory = await _purchaseHistoryService.GetPurchaseHistoryByBookIdAsync(bookId);
            if (purchaseHistory == null || purchaseHistory.Count == 0)
            {
                return NotFound($"No purchase history with book ID {bookId} was found.");
            }
            var result = purchaseHistory.Select(ph => ph.Adapt<PurchaseHistoryDetailViewModel>()).ToList();
            var viewModel = new PurchaseHistoryListViewModel
            {
                PurchaseHistories = result.Adapt<IEnumerable<PurchaseHistoryDetailViewModel>>(),
            };
            return View(viewModel);
        }
    }
}
