using BusinessLayer.DTOs.Publisher;
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
            if (publisher == null)
            {
                return null;
            }
            return DTOMapper.MapToPublisherDTO(publisher);
        }

        public async Task<PublisherCreateUpdateDTO> UpdatePublisherAsync(int id, PublisherCreateUpdateDTO publisherDTO)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher != null)
            {
                publisher.Name = publisherDTO.Name;
                await SaveAsync(true);
                return DTOMapper.MapToPublisherCreateUpdateDTO(publisher);
            }
            return null;
        }

        public async Task<IEnumerable<PublisherDTO>> SearchPublishers(string publisherName)
        {
            IQueryable<Publisher> query = _dbContext.Publishers
                .Include(b => b.Books);

            query = query.Where(b => b.Name.Contains(publisherName));

            var publishers = await query
                .ToListAsync();

            return publishers.Select(DTOMapper.MapToPublisherDTO);
        }

        public async Task<PublisherCreateUpdateDTO> CreatePublisherAsync(PublisherCreateUpdateDTO publisherDTO)
        {
            var publisher = publisherDTO.Adapt<Publisher>();
            if (publisher == null)
            {
                throw new Exception("There was an error creating Publisher");
            }
            _dbContext.Publishers.Add(publisher);
            await SaveAsync(true);
            return publisherDTO;
        }

        public async Task<bool> DeletePublisherAsync(int id)
        {
            var publisher = await _dbContext.Publishers.FindAsync(id);
            if (publisher == null)
            {
                throw new Exception("There was an error deleting Publisher");
            }
            // check if publisher is part of any book
            var books = await _dbContext.Books.Where(b => b.PublisherId == id).ToListAsync();
            if (books.Count > 0)
            {
                throw new Exception("Publisher is part of a book");
            }
            _dbContext.Publishers.Remove(publisher);
            await SaveAsync(true);
            return true;
        }
    }
}