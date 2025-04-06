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
using Microsoft.EntityFrameworkCore;
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
            
            await ordersService.AddOrder(order);

            foreach (var item in products)
            {
                Good g =await goodsService.GetGoodByName(item.Key);
                if (g == null) { return -1; }
                if (!goods.Any(x => x.ProductName == g.ProductName))
                {
                    throw new Exception("Goods not found in supplier");
                }
                if (item.Value < g.MinimumPurcheQuantity)
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
            //await ordersService.UpdateOrder(order);
            //await order.SaveChangesAsync();
            //await ordersService.AddOrder(order);
            return order.Id;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            List<Order> o=await ordersService.GetAllOrders();
            foreach (var item in o)
            {
                List<Good> goodsToOrders = goodsToOrdersService.GetGoodsToOrdersByOrderId(item.Id).GetAwaiter().GetResult();
                item.goods = goodsToOrders;
                //item.GoodsToOrders=goodsToOrdersService.GetGoodsToOrdersDerailsByOrderId(item.Id).GetAwaiter().GetResult();
            }

            return o;
        }

        public async Task<List<Order>> GetOrderByCompanyName(string company)
        {
            var supplierId = suppliersService.GetSupplierByCompany(company);
            List<Order> l = await ordersService.GetOrderBySupplierId(supplierId.GetAwaiter().GetResult().Id);
            //return l.Select(o => new OrderBLL()
            //{
            //    SupplierId = o.SupplierId,
            //    Status = o.Status
            //}).ToList();
            foreach (var item in l)
            {
                List<Good> goodsToOrders = goodsToOrdersService.GetGoodsToOrdersByOrderId(item.Id).GetAwaiter().GetResult();
                item.goods = goodsToOrders;
                //item.GoodsToOrders=goodsToOrdersService.GetGoodsToOrdersDerailsByOrderId(item.Id).GetAwaiter().GetResult();
            }
            return l;
        }

        //public async Task<Good> GetGoodsByName(string goodsName)
        //{
        //    return await goodsService.GetGoodByName(goodsName);
        //}
        public async Task<bool> OrdeCompletionConfirmation(int orderId)
        {
            //var order = await ordersService.GetOrderById(orderId);
            return await ordersService.updateOrderStatus("completed", orderId);
        }

        public async Task<bool> ConfirmationReceipOrder(int orderId)
        {
            return await ordersService.updateOrderStatus("proccess", orderId);
        }
        public async Task<Supplier> GetSupplierIdByCompany(string company)
        {
            Supplier a=await suppliersService.GetSupplierByCompany(company);
            return a; 
        }
        public async Task AddGoodsToSupplier(string company, Dictionary<string, float> goods, int min)
        {
            Supplier supplier = await  suppliersService.GetSupplierByCompany(company);

            foreach (var item in goods)
            {
                //Task<Good>  g = goodsService.GetGoodByName(item.Key);
                //if (g.GetAwaiter().GetResult() == null)
                //{

                   await goodsService.AddGood(new Good() { ProductName = item.Key, Price = item.Value, MinimumPurcheQuantity = min });
                    Task<Good>  g = goodsService.GetGoodByName(item.Key);
                //}

                await goodsToSupplierService.AddGoodsToSupplier(supplier.Id, g.GetAwaiter().GetResult().Id);
            }

        }
        public async Task<bool> proxyToSuppliers(string company, string phoneNumber)
        {
            bool b = await suppliersService.proxyToSuppliers(company, phoneNumber);
            return b;
        }

        public async Task creatSupplier(Supplier supplier)
        {
            await suppliersService.AddSupplier(supplier);
        }
    }








}

