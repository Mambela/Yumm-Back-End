using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class Category :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }


        public ICollection<Item> items { get; set; }


    }
}
