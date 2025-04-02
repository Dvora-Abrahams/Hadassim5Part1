using DAL.Models;

namespace DAL.API
{
    public interface IGoodsToOrdersService
    {
        Task AddGoodsToOrder(int orderId, int goodsId , int quantity);
        Task<List<Good>> GetGoodsToOrdersByOrderId(int orderId);
    }
}