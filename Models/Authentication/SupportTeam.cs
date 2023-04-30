using System.ComponentModel.DataAnnotations;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Authentication
{
    public class SupportTeam:BaseEntity
    {

        [Key]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string  LastName { get; set; }

        public string  IdNumber { get; set; }
        public string Gender { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
