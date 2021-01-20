using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO
{
    public class MenuGroupDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MenuId { get; set; }

        public int OrderIndex { get; set; }

    }
}
