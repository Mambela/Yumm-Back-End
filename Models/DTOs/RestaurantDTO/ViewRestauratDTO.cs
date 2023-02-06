using System;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO
{
    public class ViewRestauratDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RestaurantTypeId { get; set; }

        public string RestaurantType { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int LocationId { get; set; }

        public string Logo { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
