using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Web.Models.Home
{
    public class HomeShoppingSessionListViewModel
    {
        public List<HomeShoppingSessionViewModel> ShoppingSessions { get; set; }

        public int TotalCount { get; set; }
    }

    public class HomeShoppingSessionViewModel
    {
        public int ShoppingSessionId { get; set; }

        public int StoreId { get; set; }

        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public string StoreName { get; set; }
    }
}
