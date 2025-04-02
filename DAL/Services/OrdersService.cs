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
                throw new Exception("Order not found");
            }
            return order;
        }
        public async Task<List<Order>> GetOrderBySupplierId(int SupplierId)
        {
            List<Order> order = await _context.Orders.Where(o => o.SupplierId == SupplierId && o.Status!= "Completed").ToListAsync();

            if (order == null)
            {
                throw new Exception("Order not found");
            }
            return order;
        }
        public async Task updateOrderStatus(String status, int id)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (existingOrder != null)
            {
                existingOrder.Status = status;
                _context.Orders.Update(existingOrder);
                _context.SaveChangesAsync();
            }

        }

    }
}
