using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Supplier
{
    public int Id { get; set; } 

    public string CompanyName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public virtual ICollection<GoodsToSupplier> GoodsToSuppliers { get; set; } = new List<GoodsToSupplier>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    //public Supplier()
    //{
        
    //}

    //public Supplier(string companyName, string phoneNumber, string contactName)
    //{
    //    CompanyName = companyName;
    //    PhoneNumber = phoneNumber;
    //    ContactName = contactName;
    //}



}
