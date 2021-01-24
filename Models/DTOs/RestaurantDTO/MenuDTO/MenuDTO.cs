using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO
{
    public class MenuDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
    }
}
