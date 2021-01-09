using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO
{
    public class RestaurantTypeDTO
    {
        public int Id { get; set; }
        public string  Name { get; set; }

        public ICollection<_RestaurantDTO> restaurantDTOs { get; set; }
    }
}
