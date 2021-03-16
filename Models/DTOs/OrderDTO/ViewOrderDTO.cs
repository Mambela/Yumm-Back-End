using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yumm_Back_End.Models.DTOs.AddressDTO.Location;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Restaurants;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class ViewOrderDTO
    {

        public int Id { get; set; }

        public Decimal TotalAmount { get; set; }

        public string RestaurantName { get; set; }

        public string ClientName { get; set; }

        public string OrderStatus { get; set; }

        public bool isPaid { get; set; }

        public string Note { get; set; }

        public IList<ViewOrderProduct> OrdersProducts { get; set; }

        public UserLocationDTO Location { get; set; }

        

    }
}
