using Microsoft.AspNetCore.Mvc;
using OfficeSharedShopping.Services.Implementations.SessionRequest;
using OfficeSharedShopping.Services.Interfaces.Employee;
using OfficeSharedShopping.Services.Interfaces.SessionRequest;
using OfficeSharedShopping.Services.Interfaces.ShoppingSession;
using OfficeSharedShopping.Services.Interfaces.Store;
using OfficeSharedShopping.Web.Attributes;
using OfficeSharedShopping.Web.Models.Home;

namespace OfficeSharedShopping.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IShoppingSessionService shoppingSessionService;
        private readonly IStoreService storeService;
        private readonly ISessionRequestService sessionRequestService;

        public HomeController(IEmployeeService _employeeService, IShoppingSessionService _shoppingSessionService,
            IStoreService _storeServce, ISessionRequestService _sessionRequestService)
        {
            employeeService = _employeeService;
            shoppingSessionService = _shoppingSessionService;
            storeService = _storeServce;
            sessionRequestService = _sessionRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");

            var shoppingSessions = await shoppingSessionService.GetAllAsync();

            var inactiveSessions = shoppingSessions.ShoppingSessions
                .Where(s => !s.IsActive)
                .Select(s => new HomeShoppingSessionViewModel
                {
                    ShoppingSessionId = s.ShoppingSessionId,
                    StoreId = s.StoreId,
                    CreatedAt = s.CreatedAt,
                    CreatorId = s.CreatedByEmployeeId,
                    IsActive = s.IsActive,
                    Deadline = s.Deadline,
                    StoreName = s.StoreName
                })
                .ToList();

            var viewModel = new HomeShoppingSessionListViewModel
            {
                ShoppingSessions = inactiveSessions,
                TotalCount = inactiveSessions.Count
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int shoppingSessionId)
        {
            var sessionRequests = await sessionRequestService.GetDetailsForShoppingSessionAsync(shoppingSessionId);

            var viewModel = new ShoppingSessionDetailsListViewModel
            {
                ShoppingSessionId = shoppingSessionId,
                RequestDetails = sessionRequests.Select(r => new ShoppingRequestDetailsViewModel
                {
                    EmployeeName = r.EmployeeName,
                    ProductName = r.ProductName,
                    Quantity = r.Quantity,
                    MaxPrice = r.MaxPrice,
                    TotalOwed = r.Quantity * r.MaxPrice
                })
                .ToList()
            };

            return View(viewModel);
        }

    }
}
