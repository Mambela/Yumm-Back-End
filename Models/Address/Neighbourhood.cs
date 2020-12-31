using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Address
{
    public class Neighbourhood :BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CommuneId { get; set; }
        public Commune Commune { get; set; }

        public ICollection<Street> Streets{ get; set; }

    }
}
