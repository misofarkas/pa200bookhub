using BusinessLayer.DTOs.Publisher;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers(string? format)
        {
            var publishers = await _publisherService.GetAllPublishersAsync();
            if (publishers == null || publishers.Count == 0)
            {
                return NotFound("No publishers were found.");
            }
            return Ok(publishers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPublisher(int id, string? format)
        {
            var publisher = await _publisherService.GetPublisherAsync(id);
            if (publisher == null)
            {
                return NotFound($"No publisher with ID {id} was found.");
            }
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublisher(PublisherCreateUpdateDTO publisher)
        {
            try
            {
                var result = await _publisherService.CreatePublisherAsync(publisher);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherCreateUpdateDTO publisher)
        {
            var result = await _publisherService.UpdatePublisherAsync(id, publisher);
            if (result == null)
            {
                return NotFound($"No publisher with ID {id} was found.");
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publisherService.DeletePublisherAsync(id);
            if (result == null)
            {
                return NotFound($"No publisher with ID {id} was found.");
            }
            return Ok(result);
        }
    }
}
