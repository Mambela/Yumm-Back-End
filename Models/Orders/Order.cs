using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Orders
{
    public class Order:BaseEntity
    {

        // Major attributes used only for the order
        [Key]
        public int Id { get; set; }

        public DateTime PreparedDate { get; set; }
        public DateTime TakenOberDate { get; set; }

        public bool isPaid { get; set; }

        public Decimal Price { get; set; }

        public string Note { get; set; }


        //

        


    }
}
