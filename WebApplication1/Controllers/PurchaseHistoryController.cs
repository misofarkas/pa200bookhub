using BusinessLayer.DTOs.PurchaseHistory;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseHistoryController : ControllerBase
    {
        private readonly IPurchaseHistoryService _purchaseHistoryService;

        public PurchaseHistoryController(IPurchaseHistoryService purchaseHistoryService)
        {
            _purchaseHistoryService = purchaseHistoryService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPurchaseHistory(int id, string? format)
        {
            var purchaseHistory = await _purchaseHistoryService.GetPurchaseHistoryAsync(id);
            if (purchaseHistory == null)
            {
                return NotFound($"No purchase history with ID {id} was found.");
            }
            return Ok(purchaseHistory);
        }

        [HttpGet]
        [Route("customer/{id}")]
        public async Task<IActionResult> GetPurchaseHistoryByCustomerId(int id, string? format)
        {
            var purchaseHistory = await _purchaseHistoryService.GetPurchaseHistoryByUserIdAsync(id);
            if (purchaseHistory == null || purchaseHistory.Count == 0)
            {
                return NotFound($"No purchase history with customer ID {id} was found.");
            }
            return Ok(purchaseHistory);
        }

        [HttpGet]
        [Route("book/{id}")]
        public async Task<IActionResult> GetPurchaseHistoryByBookId(int id, string? format)
        {
            var purchaseHistory = await _purchaseHistoryService.GetPurchaseHistoryByBookIdAsync(id);
            if (purchaseHistory == null || purchaseHistory.Count == 0)
            {
                return NotFound($"No purchase history with book ID {id} was found.");
            }
            return Ok(purchaseHistory);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseHistory(PurchaseHistoryCreateUpdateDTO purchaseHistory)
        {
            try
            {
                var result = await _purchaseHistoryService.CreatePurchaseHistoryAsync(purchaseHistory);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePurchaseHistoryDate(int id, DateTime purchaseDate)
        {
            var result = await _purchaseHistoryService.UpdatePurchaseDateAsync(id, purchaseDate);
            if (result == null)
            {
                return NotFound($"No purchase history with ID {id} was found.");
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePurchaseHistory(int id)
        {
            var result = await _purchaseHistoryService.DeletePurchaseHistoryAsync(id);
            if (result)
            {
                return Ok();
            }
            return NotFound($"No purchase history with ID {id} was found.");
        }


    }
}
