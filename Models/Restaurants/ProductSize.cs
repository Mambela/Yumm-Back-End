using Yumm_Back_End.Models.Shared;

namespace Yumm_Back_End.Models.Restaurants
{
    public class ProductSize:BaseEntity
    {

        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
