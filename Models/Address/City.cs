using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Address
{
    public class City:BaseEntity
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProviceId { get; set; }
        public Province Province { get; set; }

        public ICollection<Commune> Communes { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
