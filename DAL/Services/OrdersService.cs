using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class OrdersService : IOrdersService
    {
        DB_Manager _context;
        public OrdersService()
        {
            _context = new DB_Manager();
        }

        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return null;
            }
            return order;
        }
        public async Task<List<Order>> GetOrderBySupplierId(int SupplierId)
        {
            List<Order> order = await _context.Orders.Where(o => o.SupplierId == SupplierId && o.Status != "completed").ToListAsync();

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return order;
        }
        public async Task<bool> updateOrderStatus(string status, int id)
        {

            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder != null)
            {
                if (existingOrder.Status.Equals("proccess") && status.Equals("completed") || existingOrder.Status.Equals("waiting") && status.Equals("proccess"))
                {
                    existingOrder.Status = status;
                    _context.Orders.Update(existingOrder);
                    await _context.SaveChangesAsync();
                    return true;
                }
              
            }
            return false;
        }
        public async Task UpdateOrder(Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.GoodsToOrders)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder == null)
                throw new Exception("Order not found");

            existingOrder.GoodsToOrders = order.GoodsToOrders;

            _context.Orders.Update(existingOrder);

            await _context.SaveChangesAsync();
        }


    }
}
