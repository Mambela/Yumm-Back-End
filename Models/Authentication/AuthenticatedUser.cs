using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.Authentication
{
    public class AuthenticatedUser
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Token { get; set; }

        public string role { get; set; }


    }
}
