using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class Menu:BaseEntity
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public  Restaurant Restaurant { get; set; }

        public ICollection<MenuGroup> MenuGroups { get; set; }
    }
}
