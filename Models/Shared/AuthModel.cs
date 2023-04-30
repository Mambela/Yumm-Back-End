using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.Shared
{
    public class AuthModel
    {

        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }

    }
}
