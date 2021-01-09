using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Services
{
    public class Generators
    {
        public string GenerateUniqueNumber()
        {
            string unique = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss").Trim();
            unique = unique.Replace("/", "").Replace(":", "").Replace(" ", "");
            unique = unique + "_";

            return unique;
        }
    }
}
