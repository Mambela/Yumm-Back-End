using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Clients
{
    public class Client: BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public Char Gender { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
