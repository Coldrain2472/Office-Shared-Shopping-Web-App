using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Web.Models.ShoppingSession
{
    public class ShoppingSessionListViewModel
    {
        public List<ShoppingSessionViewModel> ShoppingSessions { get; set; }

        public int TotalCount { get; set; }
    }

    public class ShoppingSessionViewModel
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
