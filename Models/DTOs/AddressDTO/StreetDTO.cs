using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.AddressDTO
{
    public class StreetDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NeighbourhoodId { get; set; }
    }
}
