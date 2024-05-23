using BusinessLayer.DTOs.PurchaseHistory;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PurchaseHistoryService : BaseService, IPurchaseHistoryService
    {
        private readonly BookHubDBContext _dbContext;

        public PurchaseHistoryService(BookHubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PurchaseHistoryDTO> GetPurchaseHistoryAsync(int id)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.Include(b => b.Book).Include(c => c.Customer).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (purchaseHistory == null)
            {
                return null;
            }
            return DTOMapper.MapToPurchaseHistoryDTO(purchaseHistory);
        }

        public async Task<List<PurchaseHistoryDTO>> GetPurchaseHistoryByUserIdAsync(int customerId)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.Include(b => b.Book).Include(c => c.Customer)
                .Where(p => p.CustomerId == customerId).ToListAsync();
            return purchaseHistory.Select(a => DTOMapper.MapToPurchaseHistoryDTO(a)).ToList();
        }
        public async Task<List<PurchaseHistoryDTO>> GetPurchaseHistoryByBookIdAsync(int bookId)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.Include(b => b.Book).Include(c => c.Customer)
                .Where(p => p.BookId == bookId).ToListAsync();
            return purchaseHistory.Select(a => DTOMapper.MapToPurchaseHistoryDTO(a)).ToList();
        }

        public async Task<PurchaseHistoryCreateDTO> CreatePurchaseHistoryAsync(PurchaseHistoryCreateDTO purchaseHistoryDTO)
        {
            var purchaseHistory = EntityMapper.MapToPurchaseHistory(purchaseHistoryDTO);
            if (purchaseHistory == null)
            {
                throw new Exception("There was an error creating PurchaseHistory");
            }
            purchaseHistory.PurchaseDate = new DateTimeOffset(2023, 10, 10, 0, 0, 0, TimeSpan.Zero); ;
            var book = await _dbContext.Books.FindAsync(purchaseHistoryDTO.BookId);
            purchaseHistory.TotalPrice = book.Price;
            purchaseHistory.Paid = false;
            _dbContext.PurchaseHistories.Add(purchaseHistory);
            await SaveAsync(true);
            return purchaseHistory.Adapt<PurchaseHistoryCreateDTO>();
        }

        public async Task<bool> DeletePurchaseHistoryAsync(int id)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.FindAsync(id);
            if (purchaseHistory == null)
            {
                return false;
            }
            _dbContext.PurchaseHistories.Remove(purchaseHistory);
            await SaveAsync(true);
            return true;
        }

        public async Task<PurchaseHistoryDTO> UpdatePurchaseHistoryAsync(int id, PurchaseHistoryUpdateDTO model)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.Include(b => b.Book).Include(c => c.Customer).Where(p => p.Id == id).FirstOrDefaultAsync();
            if (purchaseHistory != null)
            {
                purchaseHistory.PurchaseDate = model.PurchaseDate;
                purchaseHistory.Paid = model.Paid;
                purchaseHistory.TotalPrice = model.TotalPrice;

                _dbContext.Entry(purchaseHistory).State = EntityState.Modified;
                await SaveAsync(true);
                return DTOMapper.MapToPurchaseHistoryDTO(purchaseHistory);
            }
            return null;
        }
    }
}