using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeSharedShopping.Services.DTOs.SessionRequest;
using OfficeSharedShopping.Services.Interfaces.Employee;
using OfficeSharedShopping.Services.Interfaces.Product;
using OfficeSharedShopping.Services.Interfaces.SessionRequest;
using OfficeSharedShopping.Services.Interfaces.ShoppingSession;
using OfficeSharedShopping.Web.Attributes;
using OfficeSharedShopping.Web.Helpers;
using OfficeSharedShopping.Web.Models.SessionRequest;

namespace OfficeSharedShopping.Web.Controllers
{
    [Authorize]
    public class SessionRequestController : Controller
    {
        private readonly ISessionRequestService sessionRequestService;
        private readonly IEmployeeService employeeService;
        private readonly IShoppingSessionService shoppingSessionService;
        private readonly IProductService productService;


        public SessionRequestController(ISessionRequestService _sessionRequestService, IEmployeeService _employeeService, IShoppingSessionService _shoppingSessionService,
            IProductService _productService)
        {
            sessionRequestService = _sessionRequestService;
            employeeService = _employeeService;
            shoppingSessionService = _shoppingSessionService;
            productService = _productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allSessionRequests = await sessionRequestService.GetAllAsync();
            var viewModel = new SessionRequestListViewModel
            {
                SessionRequests = allSessionRequests.SessionRequests.Select(s => new SessionRequestViewModel
                {
                    CreatedAt = s.CreatedAt,
                    EmployeeId = s.EmployeeId,
                    MaxPrice = s.MaxPrice,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                    SessionRequestId = s.SessionRequestId,
                    ShoppingSessionId = s.SessionId,
                    EmployeeName = s.EmployeeName,
                    ProductName = s.ProductName
                })
                .ToList(),
                TotalCount = allSessionRequests.SessionRequests.Count,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int shoppingSessionId)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);
            var session = await shoppingSessionService.GetByIdAsync(shoppingSessionId);

            var products = await productService.GetAllAsync();
            var allProducts = products.Products;

            var viewModel = new SessionRequestViewModel
            {
                CreatedAt = session.CreatedAt,
                EmployeeId = currentUserId,
                EmployeeName = employee.Name,
                ShoppingSessionId = session.ShoppingSessionId,

                ProductsSelectList = new SelectList(allProducts, "ProductId", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionRequestViewModel model)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);

            var request = new CreateSessionRequestRequest
            {
                CreatedAt = model.CreatedAt,
                EmployeeId = model.EmployeeId,
                EmployeeName = model.EmployeeName,
                MaxPrice = model.MaxPrice,
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                Quantity = model.Quantity,
                SessionRequestId = model.SessionRequestId,
                SessionId = model.ShoppingSessionId
            };

            var response = await sessionRequestService.CreateSessionRequest(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int sessionRequestId)
        {
            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);
            var sessionRequest = await sessionRequestService.GetByIdAsync(sessionRequestId);

            if (sessionRequest == null)
            {
                return NotFound();
            }

            if (sessionRequest.EmployeeId != currentUserId)
            {
                return BadRequest();
            }

            var productsResponse = await productService.GetAllAsync();
            var products = productsResponse.Products;
            var viewModel = new SessionRequestViewModel
            {
                SessionRequestId = sessionRequest.SessionRequestId,
                ShoppingSessionId = sessionRequest.SessionId,
                EmployeeId = sessionRequest.EmployeeId,
                EmployeeName = employee.Name,
                ProductId = sessionRequest.ProductId,
                Quantity = sessionRequest.Quantity,
                MaxPrice = sessionRequest.MaxPrice,
                CreatedAt = sessionRequest.CreatedAt,


                ProductsSelectList = new SelectList(products, "ProductId", "Name", sessionRequest.ProductId)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SessionRequestViewModel model)
        {

            var currentUserId = IdHelper.GetUserId(HttpContext);
            var employee = await employeeService.GetByIdAsync(currentUserId);
            var session = await shoppingSessionService.GetByIdAsync(model.ShoppingSessionId);

            if (currentUserId != model.EmployeeId)
            {
                return RedirectToAction(nameof(Index));
            }

            var request = new UpdateSessionRequestRequest
            {
                MaxPrice = model.MaxPrice,
                Quantity = model.Quantity,
                ShoppingSessionId = model.ShoppingSessionId,
                ProductId = model.ProductId
            };
            var response = await sessionRequestService.UpdateSessionRequest(model.SessionRequestId, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
