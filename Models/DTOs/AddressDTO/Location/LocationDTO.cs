using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.AddressDTO.Location
{
    public class LocationDTO
    {
        public int Id { get; set; }

        public string  Province { get; set; }

        public string  City { get; set; }

        public string Commune { get; set; }

        public string Neighbourhood { get; set; }

        public string Street { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }
    }
}
