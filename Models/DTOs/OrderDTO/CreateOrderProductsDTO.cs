using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class CreateOrderProductsDTO
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quatity { get; set; }

        public IList<CreateOrderProductExtrasDTO> OrderProductExtras { get; set; }
    }
}
