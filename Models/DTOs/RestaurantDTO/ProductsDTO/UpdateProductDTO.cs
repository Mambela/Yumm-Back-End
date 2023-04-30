using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int Duration { get; set; }

        public int RestaurantId { get; set; }

        public int MenuGroupId { get; set; }

        public string ImagePath { get; set; }

        public IFormFile Files { get; set; }


        public List<ProductSizesDTO> Sizes { get; set; }
    }
}
