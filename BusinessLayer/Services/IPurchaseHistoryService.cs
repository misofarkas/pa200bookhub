using BusinessLayer.DTOs.PurchaseHistory;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IPurchaseHistoryService : IBaseService
    {
        Task<PurchaseHistoryDTO> GetPurchaseHistoryAsync(int id);
        Task<PurchaseHistoryCreateDTO> CreatePurchaseHistoryAsync(PurchaseHistoryCreateDTO newPurchaseHistory);
        Task<bool> DeletePurchaseHistoryAsync(int id);
        Task<List<PurchaseHistoryDTO>> GetPurchaseHistoryByUserIdAsync(int id);
        Task<List<PurchaseHistoryDTO>> GetPurchaseHistoryByBookIdAsync(int id);
        Task<PurchaseHistoryDTO> UpdatePurchaseHistoryAsync(int id, PurchaseHistoryUpdateDTO model);
    }
}
