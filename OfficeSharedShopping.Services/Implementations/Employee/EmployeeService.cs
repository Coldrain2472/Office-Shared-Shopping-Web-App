using OfficeSharedShopping.Repository.Interfaces.Employee;
using OfficeSharedShopping.Services.DTOs.Employee;
using OfficeSharedShopping.Services.Helpers;
using OfficeSharedShopping.Services.Interfaces.Employee;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace OfficeSharedShopping.Services.Implementations.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        public async Task<GetAllEmployeesResponse> GetAllAsync()
        {
            var employees = await employeeRepository.RetrieveCollectionAsync(new EmployeeFilter()).ToListAsync();
            var allEmployeesResponse = new GetAllEmployeesResponse
            {
                Employees = employees.Select(MapToEmployeeInfo).ToList(),
                TotalCount = employees.Count
            };

            return allEmployeesResponse;
        }

        public async Task<GetEmployeeResponse> GetByIdAsync(int employeeId)
        {
            var employee = await employeeRepository.RetrieveAsync(employeeId);
            return new GetEmployeeResponse
            {
                EmployeeId = employeeId,
                Department = employee.Department,
                Email = employee.Email,
                Name = employee.Name,
                Phone = employee.Phone
            };
        }

        public async Task<UpdateEmployeeResponse> UpdateFullNameAsync(UpdateNameRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NewName))
                {
                    throw new ValidationException("Full name cannot be empty");
                }

                var update = new EmployeeUpdate
                {
                    Name = new SqlString(request.NewName)
                };

                var success = await employeeRepository.UpdateAsync(request.EmployeeId, update);

                return new UpdateEmployeeResponse
                {
                    Success = success,
                    UpdatedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new UpdateEmployeeResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    UpdatedAt = DateTime.Now
                };
            }
        }

        public async Task<UpdateEmployeeResponse> UpdatePasswordAsync(UpdatePasswordRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NewPassword))
                {
                    throw new ValidationException("Password cannot be empty");
                }

                var hashedPassword = SecurityHelper.HashPassword(request.NewPassword);
                var update = new EmployeeUpdate
                {
                    Password = new SqlString(hashedPassword)
                };

                var success = await employeeRepository.UpdateAsync(request.EmployeeId, update);

                return new UpdateEmployeeResponse
                {
                    Success = success,
                    UpdatedAt = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new UpdateEmployeeResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    UpdatedAt = DateTime.Now
                };
            }
        }

        private EmployeeInfo MapToEmployeeInfo(Models.Employee employee)
        {
            return new EmployeeInfo
            {
                Department = employee.Department,
                Email = employee.Email,
                EmployeeId = employee.EmployeeId,
                Name = employee.Name,
                Phone = employee.Phone
            };
        }
    }
}
