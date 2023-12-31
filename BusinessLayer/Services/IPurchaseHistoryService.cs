using BusinessLayer.DTOs;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IPurchaseHistoryService : IBaseService
    {
        Task<PurchaseHistoryDTO> GetPurchaseHistoryAsync(int id);
        Task<PurchaseHistoryDTO> UpdatePurchaseDateAsync(int id, DateTime newPurchaseDate);
    }
}
