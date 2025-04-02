using BLL.Models;
using DAL.Models;

namespace BLL.API
{
    public interface IOrdersManagment
    {
        void AddGoodsToSupplier(string company, Dictionary<string, float> goods, int min);
        Task ConfirmationReceipOrder(int orderId);
        Task<int> CreateOrder(Dictionary<string, int> products, Order order);
        Task<List<Order>> GetAllOrders();
        Task<List<OrderBLL>> GetOrderByCompanyName(string company );
        Task OrdeCompletionConfirmation(int orderId);
        bool proxyToSuppliers(string company, string phoneNumber);
        public void creatSupplier(Supplier supplier);
    }
}