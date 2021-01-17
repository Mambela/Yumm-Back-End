using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Restaurants

{
    public class Restaurant:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int RestaurantTypeId { get; set; }
        public RestaurantType RestaurantType { get; set; }

        public virtual Menu Menu { get; set; }


        //public int LocationId { get; set; }
        //public Location Location { get; set; }

        //   public ICollection<Product> Products { get; set; }


    }
}
