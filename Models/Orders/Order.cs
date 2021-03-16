using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.Address;
using Yumm_Back_End.Models.Clients;
using Yumm_Back_End.Models.Restaurants;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Orders
{
    public class Order:BaseEntity
    {

        // Major attributes used only for the order
        [Key]
        public int Id { get; set; }

        public DateTime? PreparedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }

        public bool isPaid { get; set; }

        public Decimal Amount { get; set; }

        public string Note { get; set; }


        //
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        
        public int ClientId { get; set; }
        public Client Clients { get; set; }


        public int RestaurantId { get; set; }
        public Restaurant Restaurants { get; set; }


        public int LocationId { get; set; }
        public Location Locations { get; set; }


        public ICollection<OrdersProducts> OrdersProducts { get; set; }

    }
}
