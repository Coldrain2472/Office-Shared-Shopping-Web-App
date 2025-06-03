using OfficeSharedShopping.Repository.Interfaces.Employee;
using OfficeSharedShopping.Services.DTOs.Authentication;
using OfficeSharedShopping.Services.Helpers;
using OfficeSharedShopping.Services.Interfaces.Authentication;
using System.Data.SqlTypes;

namespace OfficeSharedShopping.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository employeeRepository;

        public AuthenticationService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Email and password are required."
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new EmployeeFilter { Email = new SqlString(request.Email) };

            var employees = await employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var employee = employees.SingleOrDefault();

            if (employee == null || employee.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid email address or password."
                };
            }

            return new LoginResponse
            {
                Success = true,
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Phone = employee.Phone,
                Email = employee.Email,
                Department = employee.Department
            };
        }
    }
}
