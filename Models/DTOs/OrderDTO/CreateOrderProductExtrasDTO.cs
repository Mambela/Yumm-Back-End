using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class CreateOrderProductExtrasDTO
    {

        public int Id { get; set; }

        public int OrdersProductsId { get; set; }

        public int ProductExtrasId  { get; set; }

        public int Quantity { get; set; }
    }
}
