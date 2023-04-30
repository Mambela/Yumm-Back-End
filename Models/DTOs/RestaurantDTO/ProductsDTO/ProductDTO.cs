using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ProductSizesDTO> ProductSizes { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }

        public int Duration { get; set; }

        public string ImagePath { get; set; }

        public int MenuGroupId { get; set; }

        public MenuGroupDTO MenuGroup { get; set; }
    }
}
