using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Web.Models.ShoppingSession
{
    public class CreateShoppingSessionViewModel
    {
        public int ShoppingSessionId { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
