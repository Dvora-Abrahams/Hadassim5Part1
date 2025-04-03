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
    public class GoodsService : IGoodsService
    {
        DB_Manager _context;
        public GoodsService()
        {
            _context = new DB_Manager();
        }

        public async Task AddGood(Good good)
        {
            _context.Goods.Add(good);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Good>> GetAllGoods()
        {
            return await _context.Goods.ToListAsync();
        }
        public async Task<Good> GetGoodByName(string name)
        {
            var  good =  await _context.Goods.FirstOrDefaultAsync(g => g.ProductName == name);
            if (good == null)
            {
                return null;
            }
            return good;
        }
        //public void UpdateGood(Good good)
        //{
        //    var existingGood = _context.Goods.FirstOrDefault(g => g.Id == good.Id);
        //    if (existingGood == null)
        //    {
        //        throw new Exception("Good not found");
        //    }
        //    _context.Goods.Update(good);
        //    _context.SaveChanges();
        //}

    }
}
