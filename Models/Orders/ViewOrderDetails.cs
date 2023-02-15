namespace Yumm_Back_End.Models.Orders
{
    public class ViewOrderDetails
    {

        public int Id { get; set; }

        public string ProductName { get; set; }

        public string Size { get; set; }

        public decimal Amount { get; set; }

        public string Note { get; set; }
    }
}
