using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Address
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Commune { get; set; }

        public string Neighbourhood{ get; set; }

        public string StreetName { get; set; }

        //public string BuildingType { get; set; }

        //public string Number { get; set; }

        //public string Description { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }

        public virtual ICollection<UsersLocations> Users2Locations { get; set; }


    }
}
