using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Models
{
    public class ShoppingSession
    {
        public int ShoppingSessionId { get; set; }

        [Required(ErrorMessage = "Store is required.")]
        public int StoreId { get; set; }

        [Required(ErrorMessage = "Creator is required.")]
        public int CreatedByEmployeeId { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Deadline {  get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
