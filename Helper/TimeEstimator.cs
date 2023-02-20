using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Helper
{
    public class TimeEstimator
    {
        public DateTime DueAt(Order order) 
        {

            DateTime dt = order.UpdatedDate;


            int prodCount = order.OrderDetails.Select(od =>od.NoOfServing).First();
            int  minutes = order.OrderDetails.Select(od => od.UpdatedDate.Minute * od.NoOfServing).Sum();

                
            if(prodCount > 1)
            return dt.AddMinutes(minutes * 0.65);


            return dt.AddMinutes(minutes);
        }
    }
}
