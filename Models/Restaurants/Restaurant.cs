using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.Restaurants

{
    public class Restaurant
    {

        public int RestaurantId { get; set; }


        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
