using Microsoft.AspNetCore.Mvc;
using BLL.API;
using BLL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SuppliersController : Controller
    {
        private readonly ILogger<SuppliersController> _logger;
        IOrdersManagment ordersManagment;

        public SuppliersController(IOrdersManagment _ordersManagment)
        {
            ordersManagment = _ordersManagment;
        }

        [HttpPost("creatSupplier")]
        public void creatSupplier([FromQuery] SuppliersBLL sup)
        {
              ordersManagment.creatSupplier(sup.Convert());
        }

        [HttpPost("RegisteredSupplier")]
        public void proxyToSuppliers([FromQuery]string company , string phone)
        {
            ordersManagment.proxyToSuppliers(company,phone);
        }

        [HttpPost("AddGoodsToSupplier")]
        public async Task AddGoodsToSupplier(string company , Dictionary<string, float> dict,int n )
        {
             await ordersManagment.AddGoodsToSupplier(company, dict, n);
        }

        [HttpGet("GetOrderByCompany")]
        public async Task<List<OrderBLL>> GetOrderByCompany(string company)
        {
            return await ordersManagment.GetOrderByCompanyName(company);
        }

        [HttpPut("ConfirmationReceipOrder")]
        public async Task ConfirmationReceipOrder(int orderId)
        {
            await ordersManagment.ConfirmationReceipOrder(orderId);
        }
        


    }
}
