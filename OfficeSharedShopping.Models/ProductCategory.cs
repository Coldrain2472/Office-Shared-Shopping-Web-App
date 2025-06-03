using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }
    }
}
