using System.ComponentModel.DataAnnotations;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.DTOs.DriversDTO
{
    public class ViewDriverDTO : BaseEntity
    {
        public int Id { get; set; }


        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public char Gender { get; set; }

        public bool Blocked { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageLink { get; set; }

        public int AddressId { get; set; }
    }
}
