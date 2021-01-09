using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Address
{
    public class UsersLocations :BaseEntity
    {
        public string UserId { get; set; }
        public int LocationId { get; set; }


        public string Name { get; set; }

        public int Unit { get; set; }


        public string Description { get; set; }


        //----------------------------------
        //Relationships

        public ApplicationUser User { get; set; }
        public Location Location { get; set; }

    }
}
