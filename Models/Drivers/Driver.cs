using System.ComponentModel.DataAnnotations;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Drivers
{
    public class Driver: BaseEntity
    {

        public int Id { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }
        
        [StringLength(100)]
        public string LastName { get; set; }
        
        public char Gender{ get; set; }


        public string UserId{ get; set; }
        public ApplicationUser User { get; set; }


        public string ImageLink { get; set; }


        public int AddressId { get; set; }


        /*
         * Associate Driver to an Order
         * Associate Driver to Location
         * **/
    }
}
