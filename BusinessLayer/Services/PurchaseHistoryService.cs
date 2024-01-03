using BusinessLayer.DTOs;
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
            return DTOMapper.MapToPurchaseHistoryDTO(purchaseHistory);
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