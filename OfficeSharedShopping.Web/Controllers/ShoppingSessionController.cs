using Microsoft.AspNetCore.Mvc;
using OfficeSharedShopping.Services.DTOs.ShoppingSession;
using OfficeSharedShopping.Services.Interfaces.Employee;
using OfficeSharedShopping.Services.Interfaces.ShoppingSession;
using OfficeSharedShopping.Services.Interfaces.Store;
using OfficeSharedShopping.Web.Attributes;
using OfficeSharedShopping.Web.Helpers;
using OfficeSharedShopping.Web.Models.ShoppingSession;

namespace OfficeSharedShopping.Web.Controllers
{
    [Authorize]
    public class ShoppingSessionController : Controller
    {
        private readonly IShoppingSessionService shoppingSessionService;
        private readonly IEmployeeService employeeService;
        private readonly IStoreService storeService;

        public ShoppingSessionController(IShoppingSessionService _shoppingSessionService, IEmployeeService _employeeService, IStoreService _storeService)
        {
            shoppingSessionService = _shoppingSessionService;
            employeeService = _employeeService;
            storeService = _storeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sessions = await shoppingSessionService.GetAllAsync();
            var viewModel = new ShoppingSessionListViewModel
            {
                ShoppingSessions = sessions.ShoppingSessions
                 .Where(s => s.IsActive)
                 .Select(s => new ShoppingSessionViewModel
                {
                    ShoppingSessionId = s.ShoppingSessionId,
                    StoreId = s.StoreId,
                    CreatedAt = s.CreatedAt,
                    CreatorId = s.CreatedByEmployeeId,
                    IsActive = s.IsActive,
                    Deadline = s.Deadline,
                    StoreName = s.StoreName
                })
                .ToList(),
                TotalCount = sessions.TotalCount
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int storeId)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);
            var store = await storeService.GetByIdAsync(storeId);

            var viewModel = new CreateShoppingSessionViewModel
            {
                StoreId = store.StoreId,
                StoreName = store.Name,
                CreatedAt = DateTime.Now,
                CreatorId = currentUserId,
                CreatorName = employee.Name,
                IsActive = true
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateShoppingSessionViewModel model)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);

            var request = new CreateShoppingSessionRequest
            {
                CreatedByEmployeeId = model.CreatorId,
                Deadline = model.Deadline,
                StoreId = model.StoreId,
                CreatedAt = model.CreatedAt,
                IsActive = model.IsActive,
                ShoppingSessionId = model.ShoppingSessionId,
                CreatedByEmployeeName = model.CreatorName,
                StoreName = model.CreatorName
            };

            var response = await shoppingSessionService.CreateShoppingSessionAsync(request);

            if (!response.Success)
            {
                ModelState.AddModelError("", response.ErrorMessage);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int shoppingSessionId)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);

            var request = new EndShoppingSessionRequest
            {
                CreatedByEmployeeId = currentUserId,
                ShoppingSessionId = shoppingSessionId
            };

            var response = await shoppingSessionService.EndShoppingSessionAsync(request);

            if (!response.Success)
            {
                TempData["ErrorMessage"] = response.ErrorMessage;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
