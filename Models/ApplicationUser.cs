using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Restaurant Restaurant { get; set; }
    }
}
