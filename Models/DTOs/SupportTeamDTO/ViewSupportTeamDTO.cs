using System.ComponentModel.DataAnnotations;

namespace Yumm_Back_End.Models.DTOs.SupportTeamDTO
{
    public class ViewSupportTeamDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

     //   public string Username { get; set; }
        public string IdNumber { get; set; }
        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }



    }
}
