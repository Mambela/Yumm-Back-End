using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Shared;
using System.ComponentModel.DataAnnotations.Schema;
using Yumm_Back_End.Models.User;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.Orders
{
    public class Order : BaseEntity
    {
        // Major attributes used only for the order
        [Key]
        public int Id { get; set; }
        public bool isPaid { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal TotalAmount { get; set; }

        public string Note { get; set; }

        public string OrderStatus { get; set; }

        public string? ProcessedByUserId { get; set; }
        public ApplicationUser? ProcessedByUser { get; set; }

        public string OrderedByUserId { get; set; }
        public ApplicationUser OrderedByUser { get; set; }
        public int ClientAddressId { get; set; }
        public ClientAddress ClientAddress { get; set; }

        public DateTime PreparedDate { get; set; }
        public DateTime TakenOderDate { get; set; }


        public List<OrderDetails> OrderDetails { get; set; }

        public string? CollectedByUserId { get; set; }

        public ApplicationUser? CollectedByUser { get; set; }


        //public int RestaurantId { get; set; }

        //public Restaurant Restaurant { get; set; }

    }
}
