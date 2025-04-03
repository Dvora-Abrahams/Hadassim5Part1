using DAL.Models;

namespace DAL.API
{
    public interface ISuppliersService
    {
        Task AddSupplier(Supplier supplier);
        Task<List<Supplier>> GetAllSuppliers();
        Task<Supplier> GetSupplierByCompany(string company);
        Task< bool> proxyToSuppliers(string company, string phoneNumber);
    }
}