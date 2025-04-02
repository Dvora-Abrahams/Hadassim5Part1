using DAL.Models;

namespace DAL.API
{
    public interface IOrdersService
    {
        Task AddOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrderBySupplierId(int SupplierId);
        Task updateOrderStatus(string status, int id);
        Task<Order> GetOrderById(int id);
    }
}