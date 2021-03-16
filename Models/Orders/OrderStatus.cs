using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Orders
{
    public class OrderStatus :BaseEntity
    {

        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

      
        public ICollection<Order> Orders { get; set; }
    }
}
