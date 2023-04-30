using System.Collections.Generic;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO
{
    public class ViewProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<ProductSizesDTO> ProductSizes { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public int Duration { get; set; }

        public string ImagePath { get; set; }

        public int MenuGroupId { get; set; }

        public string MenuGroup { get; set; }
    }
}
