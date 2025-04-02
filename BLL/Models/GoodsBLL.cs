using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class GoodsBLL
    {
        [Required, MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]

        public double Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]

        public int MinimumPurcheQuantity { get; set; }

        public Good Convert()
        {
            return new Good
            {
                ProductName = ProductName,
                Price = Price,
                MinimumPurcheQuantity = MinimumPurcheQuantity
            };
        }
    }
}
