using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Restaurant Restaurant { get; set; }

        public virtual Client Client { get; set; }


        public string Token { get; set; }

        public int OTP { get; set; }
    }
}
