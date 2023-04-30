using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yumm_Back_End.Models.DTOs.AddressDTO.Location
{
    public class CreateUserLocationDTO
    {
        public int Id { get; set; }


        //Location by the 
        public string UserId { get; set; }

        //Location Values in case there is a need to add a location.
        public int LocationId { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }


        //User Location values

        public string Name { get; set; }

        public string Unit { get; set; }

        public string Description { get; set; }


        //User Suggestion
        public string _StreetSetByUser { get; set; }

    }
}
