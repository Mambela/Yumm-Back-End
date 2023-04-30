using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.Authentication
{
    public class RegisterModelRestaurant
    {

        public string username { get; set; }

        public string Email { get; set; } //Will be used as username and email address

        public string Password { get; set; }

        public string PhoneNumber{ get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RestaurantTypeId { get; set; }

        public int LocationId { get; set; }

        public string UserRole { get; set; }

        /// <summary>
        /// /The Details below are user for the user manager 
        /// </summary>
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public string RestaurantPhoneNumber { get; set; }

        public string RestaurantEmail { get; set; }


    }
}
