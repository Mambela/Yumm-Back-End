using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.Restaurants

{
    public class Restaurant
    {

        public int RestaurantId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }


        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
