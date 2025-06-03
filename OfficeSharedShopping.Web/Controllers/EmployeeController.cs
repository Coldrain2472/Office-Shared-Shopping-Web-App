using Microsoft.AspNetCore.Mvc;
using OfficeSharedShopping.Services.Interfaces.Employee;
using OfficeSharedShopping.Web.Attributes;
using OfficeSharedShopping.Web.Models.Employee;

namespace OfficeSharedShopping.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService _employeeService)
        {
            employeeService = _employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await employeeService.GetAllAsync();
            var viewModel = new EmployeeListViewModel
            {
                Employees = employees.Employees.Select(e => new EmployeeViewModel
                {
                    EmployeeId = e.EmployeeId,
                    Department = e.Department,
                    Email = e.Email,
                    Name = e.Name,
                    Phone = e.Phone
                })
                .ToList(),
                TotalCount = employees.TotalCount
            };

            return View(viewModel);
        }
    }
}
