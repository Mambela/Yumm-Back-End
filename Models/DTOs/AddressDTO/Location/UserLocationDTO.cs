using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.AddressDTO.Location
{
    public class UserLocationDTO
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Unit { get; set; }

        public string Description { get; set; }

        public int LocationId { get; set; }

        public string City { get; set; }

        public string Commune { get; set; }

        public string Neighbourhood { get; set; }

        public string Street { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng  { get; set; }
    }
}
