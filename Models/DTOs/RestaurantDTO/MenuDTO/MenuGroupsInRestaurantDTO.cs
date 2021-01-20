using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.RestaurantDTO.MenuDTO
{
    public class MenuGroupsInRestaurantDTO
    {

        //This id is of the restaurant 
        public  int Id { get; set; }

        //Name of the Restaurant
        public string Name { get; set; }


        public int MenuId { get; set; }

        public IList<MenuGroupDTO> MenuGroup { get; set; }

    }
}
