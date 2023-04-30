using System.ComponentModel.DataAnnotations;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Restaurants
{
    public class RestaurantUser:BaseEntity
    {
        public Restaurant Restaurant { get; set; }
        public  int RestaurantId{ get; set; }

        
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string FirstName { get; set; }
        public string LastName{ get; set; }



    }
}
