using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Address
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }


        public int CityId { get; set; }
        public City City { get; set; }


        public int CommuneId { get; set; }
        public Commune Commune { get; set; }

        public int NeighbourhoodId { get; set; }
        public Neighbourhood Neighbourhood{ get; set; }

        public int StreetId { get; set; }
        public Street Street { get; set; }


        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }

        public virtual ICollection<UsersLocations> UsersLocations { get; set; }

        public virtual ICollection<Order> Orders { get; set; }





    }
}
