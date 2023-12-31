using BusinessLayer.DTOs;
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
            if (purchaseHistory != null)
            {
                return new PurchaseHistoryDTO
                {
                    Id = purchaseHistory.Id,
                    CustomerId = purchaseHistory.CustomerId,
                    BookId = purchaseHistory.BookId,
                    PurchaseDate = purchaseHistory.PurchaseDate
                };
            }
            return null;
        }

        public async Task<PurchaseHistoryDTO> UpdatePurchaseDateAsync(int id, DateTime newPurchaseDate)
        {
            var purchaseHistory = await _dbContext.PurchaseHistories.FindAsync(id);
            if (purchaseHistory != null)
            {
                purchaseHistory.PurchaseDate = newPurchaseDate;
                await SaveAsync(true);
                return new PurchaseHistoryDTO
                {
                    Id = purchaseHistory.Id,
                    CustomerId = purchaseHistory.CustomerId,
                    BookId = purchaseHistory.BookId,
                    PurchaseDate = purchaseHistory.PurchaseDate
                };
            }
            return null;
        }
    }
}