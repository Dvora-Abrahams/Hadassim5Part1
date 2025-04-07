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
        //private readonly ILogger<SuppliersController> _logger;
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
        public async Task<bool> proxyToSuppliers([FromQuery] string company, string phone)
        {
            return await ordersManagment.proxyToSuppliers(company, phone);
        }

        [HttpPost("AddGoodsToSupplier")]
        public async Task AddGoodsToSupplier(string company, [FromBody]Dictionary<string, float> dict, int n)
        {
            await ordersManagment.AddGoodsToSupplier(company, dict, n);
        }

        [HttpGet("GetOrderByCompany")]
        public async Task<IActionResult> GetOrderByCompany(string company)
        {
            var orders = await ordersManagment.GetOrderByCompanyName(company);
            return Ok(orders);
        }
        [HttpGet("GetCompletedOrderByCompany")]
        public async Task<IActionResult> GetCompletedOrderByCompany(string company)
        {
            var orders = await ordersManagment.GetCompletedOrderByCompanyName(company);
            return Ok(orders);
        }


        [HttpPut("ConfirmationReceipOrder")]
        public async Task<bool> ConfirmationReceipOrder(int orderId)
        {
             return await ordersManagment.ConfirmationReceipOrder(orderId);
        }



    }
}
