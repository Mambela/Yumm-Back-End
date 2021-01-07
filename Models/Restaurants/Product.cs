using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class Product :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string ItemName { get; set; }

       
        public decimal Price { get; set; }

        public string Size { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int Duration { get; set; }

        public string ImagePath { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }


    }
}
