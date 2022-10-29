using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class MenuGroup:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        [StringLength(2)]
        public int OrderIndex { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }


        //public ICollection<Product> Products { get; set; }

    }
}
