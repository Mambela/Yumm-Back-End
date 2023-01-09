using System;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO
{
    public class FullRestaurantDTO :BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string Description{ get; set; }

        public string RestaurantType { get; set; }


        public int NoOfOrders { get; set; }

        public string Reference { get; set; }



    }
}
