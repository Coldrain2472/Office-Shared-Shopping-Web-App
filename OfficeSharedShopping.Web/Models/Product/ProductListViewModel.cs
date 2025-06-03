namespace OfficeSharedShopping.Web.Models.Product
{
    public class ProductListViewModel
    {
        public List<ProductViewModel> Products { get; set; }

        public int TotalCount { get; set; }
    }

    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }
    }
}
