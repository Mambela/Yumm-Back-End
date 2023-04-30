using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO
{
    public class CreateProductExtrasDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Decimal Price { get; set; }

        public int ProductId { get; set; }

        
    }
}
