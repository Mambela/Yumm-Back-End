using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.RestaurantDTO.ProductsDTO;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class CreateOrderDTO
    {
        
        [Required]
        public Decimal Amount { get; set; }

        public string Note { get; set; }

        public int OrderStatusId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int LocationId { get; set; }


        public IList<CreateOrderProductsDTO> Products { get; set; }

    }
}
