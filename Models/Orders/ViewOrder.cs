using System;
using System.Collections.Generic;

namespace Yumm_Back_End.Models.Orders
{
    public class ViewOrderDTO
    {
        public int Id { get; set; }

        public DateTime OrderedDate { get; set; }

        public decimal TotalAmount{ get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public int NumberOfProducts { get; set; }

        public string ClientName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DueTime { get; set; }

        public IList<ViewOrderDetails> OrderDetails { get; set; }
    }
}
