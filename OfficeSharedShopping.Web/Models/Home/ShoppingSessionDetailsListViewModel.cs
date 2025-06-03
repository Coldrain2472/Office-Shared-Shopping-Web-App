namespace OfficeSharedShopping.Web.Models.Home
{
    public class ShoppingSessionDetailsListViewModel
    {
        public int ShoppingSessionId { get; set; }

        public List<ShoppingRequestDetailsViewModel> RequestDetails { get; set; }
    }

    public class ShoppingRequestDetailsViewModel
    {
        public string EmployeeName { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal MaxPrice { get; set; }

        public decimal TotalOwed { get; set; }
    }
}
