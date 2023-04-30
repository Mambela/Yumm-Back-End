using Microsoft.VisualBasic;
using System;

namespace Yumm_Back_End.Models.Orders
{
    public class ViewOrderDetails
    {

        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Size { get; set; }

        public decimal Amount { get; set; }

        public int PrepTime { get; set; }

        public int Quantity { get; set; }

        public string Note { get; set; }

        public string ImageLink { get; set; }

      
    }
}
