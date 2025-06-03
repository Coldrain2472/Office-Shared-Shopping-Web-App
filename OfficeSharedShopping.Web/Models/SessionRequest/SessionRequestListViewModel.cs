using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OfficeSharedShopping.Web.Models.SessionRequest
{
    public class SessionRequestListViewModel
    {
        public List<SessionRequestViewModel> SessionRequests { get; set; }

        public int TotalCount { get; set; }
    }

    public class SessionRequestViewModel
    {
        public int SessionRequestId { get; set; }

        public int ShoppingSessionId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 20, ErrorMessage = "Quantity must be between 1 and 20.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Maximum price is required.")]
        [Range(0.01, 10000.00, ErrorMessage = "Maximum price must be between 0.01 and 10000.00.")]
        [DataType(DataType.Currency)]
        public decimal MaxPrice { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        public SelectList? ProductsSelectList { get; set; }
    }
}
