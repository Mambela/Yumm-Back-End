using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Yumm_Back_End.Models.User;

namespace Yumm_Back_End.Models.DTOs.OrderDTO
{
    public class NewOrderDetailsDTO
    {
 
        [Column(TypeName = "decimal(10,2)")]
        public Decimal Amount { get; set; }
        public string Note { get; set; }

        public int NoOfServings { get; set; }
        
        public int ProductId { get; set; }

        public int ProductExtraId { get; set; }

        public string Size { get; set; }

    }
}
