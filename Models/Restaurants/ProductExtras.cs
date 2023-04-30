using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class ProductExtras:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Decimal Price { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
