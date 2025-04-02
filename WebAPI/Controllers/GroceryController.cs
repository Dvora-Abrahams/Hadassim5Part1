using Microsoft.AspNetCore.Mvc;
using BLL.API;
using BLL.Models;
namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GroceryController : ControllerBase
{
    
    private readonly ILogger<GroceryController> _logger;
    IOrdersManagment ordersManagment;

    public GroceryController( IOrdersManagment _ordersManagment)
    {
        ordersManagment = _ordersManagment;
    }

    [HttpPut("$OrderingGoods")]
    public async Task<int> CreateOrder([FromBody]string company,[FromQuery] Dictionary<string, int> products)
    {
        int supplierId= ordersManagment.GetSupplierIdByCompany(company);

        OrderBLL order = new OrderBLL()
        {
            SupplierId = supplierId
        };
        return await ordersManagment.CreateOrder(products, order.Convert());
    }
}
