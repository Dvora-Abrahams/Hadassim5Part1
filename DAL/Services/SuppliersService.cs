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
    public class SuppliersService : ISuppliersService
    {
        private readonly DB_Manager _context;
        public SuppliersService()
        {
            _context =  new DB_Manager();
        }
        public async Task AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }
        //public void DeleteSupplier(string company)
        //{
        //    var supplier = _context.Suppliers.FirstOrDefault(s => s.CompanyName == company);
        //    if (supplier != null)
        //    {
        //        _context.Suppliers.Remove(supplier);
        //        _context.SaveChanges();
        //    }
        //}

        //public void UpdateSupplier(Supplier supplier)
        //{
        //    _context.Suppliers.Update(supplier);
        //    _context.SaveChanges();
        //}
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }
        public async Task<Supplier> GetSupplierByCompany(string company)
        {
            var sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.CompanyName == company);
            if (sup == null)
            {
                return null;
            }
            return sup;
        }
        public async Task<bool> proxyToSuppliers(string company, string phoneNumber)
        {
            var sup = await _context.Suppliers.FirstOrDefaultAsync(s => s.CompanyName == company && s.PhoneNumber == phoneNumber);
            if(sup == null)
                return false;
            return true;
        }





    }
}
