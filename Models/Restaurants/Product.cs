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
        public string Name { get; set; }


        [StringLength(500)]
        public string Description { get; set; }

        public int categoryId { get; set; }
        public Category category { get; set; }

        public int Duration { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }

        public int MenuGroupId { get; set; }
        public MenuGroup MenuGroup { get; set; }


        public ICollection<ProductExtras> ProductsExtras { get; set; }


        public ICollection<ProductSize> ProductSizes { get; set; }

    }
}
