using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO
{
    public class _RestaurantDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public int RestaurantTypeId { get; set; }

    }
}
