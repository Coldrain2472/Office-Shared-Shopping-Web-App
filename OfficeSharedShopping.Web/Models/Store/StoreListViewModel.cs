namespace OfficeSharedShopping.Web.Models.Store
{
    public class StoreListViewModel
    {
        public List<StoreViewModel> Stores { get; set; }

        public int TotalCount { get; set; }
    }

    public class StoreViewModel
    {
        public int StoreId { get; set; }

        public string Name { get; set; }
    }
}
