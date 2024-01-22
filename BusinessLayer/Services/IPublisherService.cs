using BusinessLayer.DTOs.Publisher;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IPublisherService : IBaseService
    {
        Task<List<PublisherDTO>> GetAllPublishersAsync();
        Task<PublisherDTO> GetPublisherAsync(int id);
        Task<PublisherCreateUpdateDTO> UpdatePublisherAsync(int id, PublisherCreateUpdateDTO publisher);
        Task<PublisherCreateUpdateDTO> CreatePublisherAsync(PublisherCreateUpdateDTO publisher);
        Task<bool> DeletePublisherAsync(int id);
    }
}