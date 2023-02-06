using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Runtime;
using Yumm_Back_End.Models.Shared;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.Orders
{
    public class OrderDetails:BaseEntity
    {
        public int Id { get; set; }

        public string Size { get; set; }

        public int NoOfServing { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal Amount { get; set; }

        public int OrderId { get; set; }
       public Order Order { get; set; }

        public string ?Note{ get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
