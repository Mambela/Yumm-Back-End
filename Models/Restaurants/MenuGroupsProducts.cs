using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Yumm_Back_End.Models.Orders;

namespace Yumm_Back_End.Models.Restaurants
{
    public class MenuGroupsProducts
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }


        [Key, Column(Order = 1)]
        public int MenuGroupId { get; set; }


        public Product Product { get; set; }
        public MenuGroup MenuGroup { get; set; }
    }
}
