using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Orders
{
    public class OrdersProducts :BaseEntity
    {

        [Key,Column(Order =0)]
        public int Id { get; set; }


        public int OrderId { get; set; }
       
        public int ProductId { get; set; }

        public int Quatity { get; set; }


        //---------------------------------
        // Relationship Order and Product
        public  Order Order { get; set; }

        public  Product Product { get; set; }


        //---------------------------------
        // Relationship Product Extra 

        public ICollection<OrderProductExtras> OrderProductExtras { get; set; }

    }
}
