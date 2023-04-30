using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.AddressDTO.Location
{
    public class LocationDTO
    {
        public int Id { get; set; }

        public string Street { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }
    }
}
