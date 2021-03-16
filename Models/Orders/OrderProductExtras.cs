using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Orders
{
    public class OrderProductExtras:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int OrdersProductsId { get; set; }
        public OrdersProducts OrdersProducts { get; set; }



        public int ProductExtrasId { get; set; }
        public ProductExtras ProductExtras { get; set; }

        public int Quantity { get; set; }

        
        

    }
}
