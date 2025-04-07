using BLL.Models;
using DAL.Models;

namespace BLL.API
{
    public interface IOrdersManagment
    {
        Task<List<Order>> GetCompletedOrderByCompanyName(string company);
        Task AddGoodsToSupplier(string company, Dictionary<string, float> goods, int min);
        Task<bool> ConfirmationReceipOrder(int orderId);
        Task<int> CreateOrder(Dictionary<string, int> products, Order order);
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrderByCompanyName(string company );
        Task<bool> OrdeCompletionConfirmation(int orderId);
        Task<bool> proxyToSuppliers(string company, string phoneNumber);
         Task creatSupplier(Supplier supplier);
         Task<Supplier> GetSupplierIdByCompany(string company);
        Task<List<Order>> GetAllWaitingOrders();
    }
}