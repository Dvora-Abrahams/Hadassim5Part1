using DAL.Models;

namespace DAL.API
{
    public interface IGoodsToSupplierService
    {
        Task AddGoodsToSupplier(int supplierId, int goodsId);
        Task<List<Good>> GetGoodsToSupplierBySupplierId(int SupplierId);
    }
}