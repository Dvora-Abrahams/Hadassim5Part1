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

        [HttpPut("creatSupplier")]
        public void creatSupplier([FromQuery] SuppliersBLL sup)
        {
              ordersManagment.creatSupplier(sup.Convert());
        }

        [HttpPut("RegisteredSupplier")]
        public void proxyToSuppliers([FromQuery]string company , string phone)
        {
            ordersManagment.proxyToSuppliers(company,phone);
        }

        [HttpPut("AddGoodsToSupplier")]
        public async Task AddGoodsToSupplier(string company , Dictionary<string, float> dict,int n )
        {
            ordersManagment.AddGoodsToSupplier(company, dict, 1);
        }

        [HttpGet("GetOrderByCompany")]
        public async Task<List<OrderBLL>> GetOrderByCompany(string company)
        {
            return await ordersManagment.GetOrderByCompanyName(company);
        }


    }
}
