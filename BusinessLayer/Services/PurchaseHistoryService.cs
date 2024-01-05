using BusinessLayer.DTOs.PurchaseHistory;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
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
            var purchaseHistory = await _dbContext.PurchaseHistories.FindAsync(id);
            if (purchaseHistory == null)
            {
                return null;
            }
            return DTOMapper.MapToPurchaseHistoryDTO(purchaseHistory);
        }

        public async Task<List<PurchaseHistoryDTO>> GetPurchaseHistoryByUserIdAsync(int customerId)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories
                .Where(p => p.CustomerId == customerId).ToListAsync();
            return purchaseHistory.Select(a => DTOMapper.MapToPurchaseHistoryDTO(a)).ToList();
        }
        public async Task<List<PurchaseHistoryDTO>> GetPurchaseHistoryByBookIdAsync(int bookId)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories
                .Where(p => p.BookId == bookId).ToListAsync();
            return purchaseHistory.Select(a => DTOMapper.MapToPurchaseHistoryDTO(a)).ToList();
        }

        public async Task<PurchaseHistoryCreateUpdateDTO> CreatePurchaseHistoryAsync(PurchaseHistoryCreateUpdateDTO purchaseHistoryDTO)
        {
            var purchaseHistory = EntityMapper.MapToPurchaseHistory(purchaseHistoryDTO);
            if (purchaseHistory == null)
            {
                throw new Exception("There was an error creating PurchaseHistory");
            }
            _dbContext.PurchaseHistories.Add(purchaseHistory);
            await SaveAsync(true);
            return purchaseHistoryDTO;
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

        public async Task<PurchaseHistoryDTO> UpdatePurchaseDateAsync(int id, DateTime newPurchaseDate)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.FindAsync(id);
            if (purchaseHistory != null)
            {
                purchaseHistory.PurchaseDate = newPurchaseDate;
                await SaveAsync(true);
                return DTOMapper.MapToPurchaseHistoryDTO(purchaseHistory);
            }
            return null;
        }
    }
}