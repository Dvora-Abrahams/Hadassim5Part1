using DAL.Models;

namespace DAL.API
{
    public interface IOrdersService
    {
        Task<List<Order>> GetAllOrderBySupplierId(int SupplierId);
        Task AddOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrderBySupplierId(int SupplierId);
        Task<bool> updateOrderStatus(string status, int id);
        Task<Order> GetOrderById(int id);
         Task UpdateOrder(Order order);
        
    }
}