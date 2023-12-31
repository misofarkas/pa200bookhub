using BusinessLayer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PublisherService : BaseService, IPublisherService
    {
        private readonly BookHubDBContext _dbContext;

        public PublisherService(BookHubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PublisherDTO>> GetAllPublishersAsync()
        {
            var publishers = await _dbContext.Publishers.ToListAsync();
            return publishers.Select(p => new PublisherDTO { Id = p.Id, Name = p.Name }).ToList();
        }

        public async Task<PublisherDTO> GetPublisherAsync(int id)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher != null)
            {
                return new PublisherDTO { Id = publisher.Id, Name = publisher.Name };
            }
            return null;
        }

        public async Task<PublisherDTO> UpdatePublisherAsync(int id, PublisherDTO publisherDTO)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher != null)
            {
                publisher.Name = publisherDTO.Name;
                await SaveAsync(true);
                return new PublisherDTO { Id = publisher.Id, Name = publisher.Name };
            }
            return null;
        }
    }
}