using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.Address
{
    public class UsersLocations :BaseEntity
    {
        /*----------------------------------
         * KEYS OF THE TABLE
         * --------------------------------**/

        [Key,Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int LocationId { get; set; }


        /*-----------------------------------
         * Fields on the Table
         * ----------------------------------**/

        [Required]
        public string Name { get; set; }

        [MaxLength(10)]
        public string Unit { get; set; }

        [Required]
        public string Description { get; set; }


        public string _StreetSetByUser { get; set; }

        public Decimal Lat { get; set; }

        public Decimal Lng { get; set; }


        //----------------------------------
        //Relationships

        public virtual ApplicationUser User { get; set; }
        public virtual Location Location { get; set; }

    }
}
