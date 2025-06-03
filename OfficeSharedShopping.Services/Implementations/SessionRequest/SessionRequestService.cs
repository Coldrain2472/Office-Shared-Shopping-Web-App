using OfficeSharedShopping.Repository.Interfaces;
using OfficeSharedShopping.Repository.Interfaces.Employee;
using OfficeSharedShopping.Repository.Interfaces.SessionRequest;
using OfficeSharedShopping.Repository.Interfaces.ShoppingSession;
using OfficeSharedShopping.Services.DTOs.SessionRequest;
using OfficeSharedShopping.Services.Interfaces.SessionRequest;

namespace OfficeSharedShopping.Services.Implementations.SessionRequest
{
    public class SessionRequestService : ISessionRequestService
    {
        private readonly ISessionRequestRepository sessionRequestRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IShoppingSessionRepository shoppingSessionRepository;
        private readonly IProductRepository productRepository;

        public SessionRequestService(ISessionRequestRepository _sessionRequestRepository, IEmployeeRepository _employeeRepository,
            IShoppingSessionRepository _shoppingSessionRepository, IProductRepository _productRepository)
        {
            sessionRequestRepository = _sessionRequestRepository;
            employeeRepository = _employeeRepository;
            shoppingSessionRepository = _shoppingSessionRepository;
            productRepository = _productRepository;
        }

        public async Task<CreateSessionRequestResponse> CreateSessionRequest(CreateSessionRequestRequest request)
        {
            var employee = await employeeRepository.RetrieveAsync(request.EmployeeId);
            if (employee == null)
            {
                return new CreateSessionRequestResponse
                {
                    Success = false,
                    ErrorMessage = "Employee not found."
                };
            }

            var session = shoppingSessionRepository.RetrieveAsync(request.SessionId);
            if (session == null)
            {
                return new CreateSessionRequestResponse
                {
                    Success = false,
                    ErrorMessage = "Shopping session not found."
                };
            }

            var product = productRepository.RetrieveAsync(request.ProductId);
            if (product == null)
            {
                return new CreateSessionRequestResponse
                {
                    Success = false,
                    ErrorMessage = "Product not found."
                };
            }

            var sessionRequest = new Models.SessionRequest
            {
                SessionId = request.SessionId,
                CreatedAt = DateTime.Now,
                EmployeeId = request.EmployeeId,
                MaxPrice = request.MaxPrice,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            var sessionRequestId = await sessionRequestRepository.CreateAsync(sessionRequest);
            return new CreateSessionRequestResponse
            {
                Success = true,
                SessionRequestId = sessionRequestId
            };
        }

        public async Task<GetAllSessionRequestsResponse> GetAllAsync()
        {
            var requests = await sessionRequestRepository.RetrieveCollectionAsync(new SessionRequestFilter()).ToListAsync();
            var employeeIds = requests.Select(r => r.EmployeeId).Distinct();
            var productIds = requests.Select(r => r.ProductId).Distinct();

            var employeeNames = new Dictionary<int, string>();
            foreach (var id in employeeIds)
            {
                var employee = await employeeRepository.RetrieveAsync(id);
                if (employee != null)
                {
                    employeeNames[id] = employee.Name;
                }
            }

            var productNames = new Dictionary<int, string>();
            foreach (var id in productIds)
            {
                var product = await productRepository.RetrieveAsync(id);
                if (product != null)
                {
                    productNames[id] = product.Name;
                }
            }

            var allRequestsResponse = new GetAllSessionRequestsResponse
            {
                SessionRequests = requests.Select(s => new SessionRequestInfo
                {
                    CreatedAt = s.CreatedAt,
                    EmployeeId = s.EmployeeId,
                    EmployeeName = employeeNames.ContainsKey(s.EmployeeId) ? employeeNames[s.EmployeeId] : "Unknown",
                    MaxPrice = s.MaxPrice,
                    ProductId = s.ProductId,
                    ProductName = productNames.ContainsKey(s.ProductId) ? productNames[s.ProductId] : "Unknown",
                    Quantity = s.Quantity,
                    SessionId = s.SessionId,
                    SessionRequestId = s.SessionRequestId
                }).ToList()
            };

            return allRequestsResponse;
        }

        public async Task<GetSessionRequestResponse> GetByIdAsync(int requestId)
        {
            var request = await sessionRequestRepository.RetrieveAsync(requestId);
            var employee = await employeeRepository.RetrieveAsync(request.EmployeeId);
            var product = await productRepository.RetrieveAsync(request.ProductId);

            return new GetSessionRequestResponse
            {
                CreatedAt = request.CreatedAt,
                EmployeeId = request.EmployeeId,
                EmployeeName = employee.Name,
                MaxPrice = request.MaxPrice,
                ProductId = request.ProductId,
                ProductName = product.Name,
                Quantity = request.Quantity,
                SessionId = request.SessionId,
                SessionRequestId = request.SessionRequestId
            };
        }

        public async Task<UpdateSessionRequestResponse> UpdateSessionRequest(int sessionRequestId, UpdateSessionRequestRequest request)
        {
            var sessionRequest = await sessionRequestRepository.RetrieveAsync(sessionRequestId);
            var shoppingSession = await shoppingSessionRepository.RetrieveAsync(request.ShoppingSessionId);
            if (shoppingSession.Deadline > DateTime.Now)
            {
                shoppingSession.IsActive = true;
            }
            if (sessionRequest == null)
            {
                return new UpdateSessionRequestResponse
                {
                    Success = false,
                    ErrorMessage = "There is no request for that session."
                };
            }
            if (!shoppingSession.IsActive)
            {
                return new UpdateSessionRequestResponse
                {
                    Success = false,
                    ErrorMessage = "Shopping Session not found."
                };
            }
            var update = new SessionRequestUpdate
            {
                MaxPrice = request.MaxPrice,
                Quantity = request.Quantity,
                ProductId = request.ProductId
            };

            var success = await sessionRequestRepository.UpdateAsync(sessionRequestId, update);

            return new UpdateSessionRequestResponse
            {
                Success = true,
                ErrorMessage = success ? null : "Update failed."
            };
        }

        public async Task<List<SessionRequestDetails>> GetDetailsForShoppingSessionAsync(int shoppingSessionId)
        {
            var filter = new SessionRequestFilter
            {
                SessionId = shoppingSessionId
            };

            var requests = await sessionRequestRepository.RetrieveCollectionAsync(filter).ToListAsync();
            List<SessionRequestDetails> requestDetailsInfo = new();
            foreach (var request in requests)
            {
                var employee = await employeeRepository.RetrieveAsync(request.EmployeeId);
                var product = await productRepository.RetrieveAsync(request.ProductId);
                requestDetailsInfo.Add(new SessionRequestDetails
                {
                    EmployeeName = employee?.Name ?? "Unknown",
                    ProductName =product.Name ?? "Unknown",
                    Quantity = request.Quantity,
                    MaxPrice = request.MaxPrice
                });
            }

            return requestDetailsInfo;
        }

    }
}
