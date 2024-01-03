using BusinessLayer.DTOs;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Mapster;
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
            return publishers.Select(DTOMapper.MapToPublisherDTO).ToList();
        }

        public async Task<PublisherDTO> GetPublisherAsync(int id)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            return DTOMapper.MapToPublisherDTO(publisher);
        }

        public async Task<PublisherDTO> UpdatePublisherAsync(int id, PublisherDTO publisherDTO)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher != null)
            {
                publisher.Name = publisherDTO.Name;
                await SaveAsync(true);
                return DTOMapper.MapToPublisherDTO(publisher);
            }
            return null;
        }
    }
}