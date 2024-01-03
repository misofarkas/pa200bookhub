using BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IPublisherService : IBaseService
    {
        Task<List<PublisherDTO>> GetAllPublishersAsync();
        Task<PublisherDTO> GetPublisherAsync(int id);
        Task<PublisherDTO> UpdatePublisherAsync(int id, PublisherDTO publisher);
    }
}