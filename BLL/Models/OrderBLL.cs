using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class OrderBLL
    {
       

        [Required]
        public int SupplierId { get; set; }
        //[Required]
        //public virtual ICollection<GoodsToOrder> GoodsToOrders { get; set; } = new List<GoodsToOrder>();
        public string Status { get; set; } = "waiting";



        public Order Convert()
        {
            return new Order
            {
                SupplierId = SupplierId,
                Status = Status

            };
        }
    }
}

