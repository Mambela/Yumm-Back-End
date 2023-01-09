using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.DTOs.DriversDTO
{
    public class CreateDriverDTO
    {

        public int Id { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        public char Gender { get; set; }

        public string Username { get; set; }

        public string Email { get; set; } //Will be used as username and email address

        public string Password { get; set; }

        public string Confirmation_Password { get; set; }

        public string PhoneNumber { get; set; }
        public string ? ImageLink { get; set; }

        public int AddressId { get; set; } = 0;

        public IFormFile Files { get; set; }


    }
}
