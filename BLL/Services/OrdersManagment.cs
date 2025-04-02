using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.API;
using BLL.Models;
using DAL.API;
using DAL.Models;
using DAL.Services;
namespace BLL.Services
{
    public class OrdersManagment : IOrdersManagment
    {
        private readonly IOrdersService ordersService;
        private readonly IGoodsService goodsService;
        private readonly ISuppliersService suppliersService;
        private readonly IGoodsToOrdersService goodsToOrdersService;
        private readonly IGoodsToSupplierService goodsToSupplierService;
        public OrdersManagment(IOrdersService _ordersService, IGoodsService _goodService, ISuppliersService _suppliersService, IGoodsToOrdersService _goodsToOrdersService, IGoodsToSupplierService _goodsToSupplierService)
        {
            ordersService = _ordersService;
            goodsService = _goodService;
            suppliersService = _suppliersService;
            goodsToOrdersService = _goodsToOrdersService;
            goodsToSupplierService = _goodsToSupplierService;
        }
        public async Task<int> CreateOrder(Dictionary<string, int> products, Order order)
        {
            List<Good> goods = await goodsToSupplierService.GetGoodsToSupplierBySupplierId(order.SupplierId);
            List<GoodsToOrder> goodsToOrders = new List<GoodsToOrder>();
            foreach (var item in products)
            {
                Task<Good> g = goodsService.GetGoodByName(item.Key);
                if (g == null) { return -1; }
                if (!goods.Contains(g.GetAwaiter().GetResult()))
                {
                    throw new Exception("Goods not found in supplier");
                }
                if (item.Value < g.GetAwaiter().GetResult().MinimumPurcheQuantity)
                {
                    throw new Exception("Quantity is less than minimum purchase quantity");
                }

                GoodsToOrder gto = new GoodsToOrder()
                {
                    GoodsId = g.Id,
                    OrdersId = order.Id,
                    Quantity = item.Value

                };
                goodsToOrders.Add(gto);
                await goodsToOrdersService.AddGoodsToOrder(order.Id, g.Id, item.Value);


            }
            order.GoodsToOrders = goodsToOrders;
            await ordersService.AddOrder(order);
            return order.Id;
        }

        //public async Task<List<Order>> GetAllOrders()
        //{
        //    return await ordersService.GetAllOrders();
        //}

        public async Task<List<OrderBLL>> GetOrderByCompanyName(string company)
        {
            var supplierId = suppliersService.GetSupplierByCompany(company);
            List<Order> l = await ordersService.GetOrderBySupplierId(supplierId.Id);
            return l.Select(o => new OrderBLL()
            {
                SupplierId = o.SupplierId,
                Status = o.Status
            }).ToList();
        }

        //public async Task<Good> GetGoodsByName(string goodsName)
        //{
        //    return await goodsService.GetGoodByName(goodsName);
        //}
        public async Task OrdeCompletionConfirmation(int orderId)
        {
            //var order = await ordersService.GetOrderById(orderId);
            await ordersService.updateOrderStatus("Completed", orderId);
        }

        public async Task ConfirmationReceipOrder(int orderId)
        {
            await ordersService.updateOrderStatus("proccess", orderId);
        }
        public int GetSupplierIdByCompany(string company)
        {
            return suppliersService.GetSupplierByCompany(company).Id;
        }
        public void AddGoodsToSupplier(string company, Dictionary<string, float> goods, int min)
        {
            Task<Supplier> supplier = suppliersService.GetSupplierByCompany(company);

            foreach (var item in goods)
            {
                //Task<Good>  g = goodsService.GetGoodByName(item.Key);
                //if (g.GetAwaiter().GetResult() == null)
                //{

                    goodsService.AddGood(new Good() { ProductName = item.Key, Price = item.Value, MinimumPurcheQuantity = min });
                    Task<Good>  g = goodsService.GetGoodByName(item.Key);
                //}

                goodsToSupplierService.AddGoodsToSupplier(supplier.GetAwaiter().GetResult().Id, g.GetAwaiter().GetResult().Id);
            }

        }
        public bool proxyToSuppliers(string company, string phoneNumber)
        {
            return suppliersService.proxyToSuppliers(company, phoneNumber);
        }

        public void creatSupplier(Supplier supplier)
        {
            suppliersService.AddSupplier(supplier);
        }
    }








}

