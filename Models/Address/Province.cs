using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Address
{
    public class Province :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

    }
}
