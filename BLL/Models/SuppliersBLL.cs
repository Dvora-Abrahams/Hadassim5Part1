using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Models
{
    public class SuppliersBLL
    {

        [Required , MaxLength(50)]
        public string CompanyName { get; set; }

        [Required , Phone]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(50)]
        public string ContactName { get; set; }

        public Supplier Convert()
        {
            return new Supplier
            {
                CompanyName = CompanyName,
                PhoneNumber = PhoneNumber,
                ContactName = ContactName
            };
        }
        
    }
}


