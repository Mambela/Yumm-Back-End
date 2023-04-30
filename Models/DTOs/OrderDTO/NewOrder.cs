using System;
using System.Collections.Generic;
using Yumm_Back_End.Models.Orders;
using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class NewOrderDTO
    {
        public DateTime OrderDate { get; set; }

        public string OrderedByUserId { get; set; }

        public int LocationId { get; set; }

        public List<NewOrderDetailsDTO> OrderDetails { get; set; }

        public string Note { get; set; }


    }
}
