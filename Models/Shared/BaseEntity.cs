using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.Shared
{
    public class BaseEntity
    {

        public DateTime CreateDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
