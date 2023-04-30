using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.DTOs.ClientDTO
{
    public class ClientDTO
    {
         public int Id { get; set; }

        public string FirstName { get; set; }
    
        public string LastName { get; set; }

        public Char Gender { get; set; }

        public string PhoneNumber { get; set; }

        
    }
}
