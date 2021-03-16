using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class ViewOrderProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public string Size { get; set; }

       

        public string Note { get; set; }

        public string Description { get; set; }

        public IList<ViewOrderProductExtras> OrderExtras { get; set; }


    }
}
