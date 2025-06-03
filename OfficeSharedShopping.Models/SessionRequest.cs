using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Models
{
    public class SessionRequest
    {
        public int SessionRequestId { get; set; }

        [Required(ErrorMessage = "Shopping session is required.")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 20, ErrorMessage = "Quantity must be between 1 and 20.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Maximum price is required.")]
        [Range(0.01, 10000.00, ErrorMessage = "Maximum price must be between 0.01 and 10000.00.")]
        [DataType(DataType.Currency)]
        public decimal MaxPrice { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
