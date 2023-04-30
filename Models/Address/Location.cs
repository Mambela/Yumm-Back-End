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

        public int StreetId { get; set; }
        public Street Street { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }

        public virtual ICollection<UsersLocations> UsersLocations { get; set; }

    }
}
