using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
