using Microsoft.AspNetCore.Mvc;
using BLL.API;
using BLL.Models;
using DAL.Models;
namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GroceryController : ControllerBase
{
    
    //private readonly ILogger<GroceryController> _logger;
    IOrdersManagment ordersManagment;

    public GroceryController( IOrdersManagment _ordersManagment)
    {
        ordersManagment = _ordersManagment;
    }

    [HttpPost("OrderingGoods")]
    public async Task<int> CreateOrder([FromBody]string company,[FromQuery] Dictionary<string, int> products)
    {
        //var supplierId= ordersManagment.GetSupplierIdByCompany(company).GetAwaiter().GetResult();

         OrderBLL order = new OrderBLL()
        {
            //Supplier = ordersManagment.GetSupplierIdByCompany(company).GetAwaiter().GetResult(),
            SupplierId = ordersManagment.GetSupplierIdByCompany(company).GetAwaiter().GetResult().Id

        };
        
        return await ordersManagment.CreateOrder(products, order.Convert());
    }
    [HttpPost("OrdeCompletionConfirmation")]
    public Task OrdeCompletionConfirmation([FromBody]int orderNum)
    {
        return ordersManagment.OrdeCompletionConfirmation(orderNum);
    }

    [HttpGet("GetAllOrders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await ordersManagment.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("ConfirmationReceipOrder")]
    public async Task ConfirmationReceipOrder(int orderNum)
    {
         await ordersManagment.ConfirmationReceipOrder(orderNum);
    }
}
