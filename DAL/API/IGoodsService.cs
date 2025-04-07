using DAL.Models;

namespace DAL.API
{
    public interface IGoodsService
    {
        Task AddGood(Good good);
        Task<List<Good>> GetAllGoods();
        Task<Good> GetGoodByName(string name);
        Task<Good> GetGoodByNameAndPrice(string name, double price);
    }
}