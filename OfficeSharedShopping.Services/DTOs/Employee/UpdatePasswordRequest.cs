namespace OfficeSharedShopping.Services.DTOs.Employee
{
    public class UpdatePasswordRequest
    {
        public int EmployeeId { get; set; }

        public string NewPassword { get; set; }
    }
}
