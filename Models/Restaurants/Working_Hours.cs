using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class Working_Hours:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Weekday { get; set; }


        [Required]
        public DateTime OpeningHours { get; set; }
        
        [Required]
        public DateTime ClosingHours { get; set; }


        [Required]
        public Restaurant Restaurant { get; set; }

 
    }
}
