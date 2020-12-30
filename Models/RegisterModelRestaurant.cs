using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models
{
    public class RegisterModelRestaurant
    {

        public string Email { get; set; } //Will be used as username and email address

        public string Password { get; set; }

        public string PhoneNumber{ get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


    }
}
