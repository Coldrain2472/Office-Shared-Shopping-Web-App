using OfficeSharedShopping.Repository.Interfaces.Employee;
using OfficeSharedShopping.Repository.Interfaces.ShoppingSession;
using OfficeSharedShopping.Repository.Interfaces.Store;
using OfficeSharedShopping.Services.DTOs.ShoppingSession;
using OfficeSharedShopping.Services.Interfaces.ShoppingSession;
using System.Data.SqlTypes;

namespace OfficeSharedShopping.Services.Implementations.ShoppingSession
{
    public class ShoppingSessionService : IShoppingSessionService
    {
        private readonly IShoppingSessionRepository shoppingSessionRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IStoreRepository storeRepository;

        public ShoppingSessionService(IShoppingSessionRepository _shoppingSessionRepository, IEmployeeRepository _employeeRepository,
            IStoreRepository _storeRepository)
        {
            shoppingSessionRepository = _shoppingSessionRepository;
            employeeRepository = _employeeRepository;
            storeRepository = _storeRepository;
        }

        public async Task<CreateShoppingSessionResponse> CreateShoppingSessionAsync(CreateShoppingSessionRequest request)
        {
            var employee = await employeeRepository.RetrieveAsync(request.CreatedByEmployeeId);
            if (employee == null)
            {
                return new CreateShoppingSessionResponse
                {
                    Success = false,
                    ErrorMessage = "Employee not found."
                };
            }

            var store = await storeRepository.RetrieveAsync(request.StoreId);
            if (store == null)
            {
                return new CreateShoppingSessionResponse
                {
                    Success = false,
                    ErrorMessage = "Store not found."
                };
            }

            var existingSession = await shoppingSessionRepository.RetrieveAsync(request.StoreId);
            if (existingSession != null)
            {
                return new CreateShoppingSessionResponse
                {
                    Success = false,
                    ErrorMessage = "A shopping session for that store already exists!"
                };
            }

            var shoppingSession = new Models.ShoppingSession
            {
                CreatedByEmployeeId = request.CreatedByEmployeeId,
                StoreId = request.StoreId,
                CreatedAt = DateTime.Now,
                IsActive = true,
                Deadline = request.Deadline
            };

            var shoppingSessionId = await shoppingSessionRepository.CreateAsync(shoppingSession);

            return new CreateShoppingSessionResponse
            {
                Success = true,
                ShoppingSessionId = shoppingSessionId
            };
        }

        public async Task<EndShoppingSessionResponse> EndShoppingSessionAsync(EndShoppingSessionRequest request)
        {
            var shoppingSession = await shoppingSessionRepository.RetrieveAsync(request.ShoppingSessionId);
            if (shoppingSession.CreatedByEmployeeId != request.CreatedByEmployeeId)
            {
                return new EndShoppingSessionResponse
                {
                    Success = false,
                    ErrorMessage = "Only the creator can end the shopping session."
                };
            }

            var update = new ShoppingSessionUpdate
            {
                IsActive = SqlBoolean.False,
                Deadline = new SqlDateTime(DateTime.Now)
            };

            var isUpdated = await shoppingSessionRepository.UpdateAsync(request.ShoppingSessionId, update);

            if (isUpdated)
            {
                return new EndShoppingSessionResponse
                {
                    Success = true,
                    EndTime = (DateTime)update.Deadline
                };
            }
            else
            {
                return new EndShoppingSessionResponse
                {
                    Success = false,
                    ErrorMessage = "Unable to end the shopping session."
                };
            }
        }

        public async Task<GetAllShoppingSessionsResponse> GetAllAsync()
        {
            var sessions = await shoppingSessionRepository.RetrieveCollectionAsync(new ShoppingSessionFilter()).ToListAsync();

            var storeIds = sessions.Select(s => s.StoreId).Distinct();
            var stores = new Dictionary<int, string>();
            foreach (var storeId in storeIds)
            {
                var store = await storeRepository.RetrieveAsync(storeId);
                if (store != null)
                {
                    stores[storeId] = store.Name;
                }
            }

            var allSessionResponse = new GetAllShoppingSessionsResponse
            {
                ShoppingSessions = sessions.Select(s => new ShoppingSessionInfo
                {
                    CreatedAt = s.CreatedAt,
                    IsActive = s.IsActive,
                    ShoppingSessionId = s.ShoppingSessionId,
                    Deadline = s.Deadline,
                    CreatedByEmployeeId = s.CreatedByEmployeeId,
                    StoreId = s.StoreId,
                    StoreName = stores.ContainsKey(s.StoreId) ? stores[s.StoreId] : "Unknown Store"
                })
                .ToList(),
                TotalCount = sessions.Count()
            };
            return allSessionResponse;
        }

        public async Task<GetShoppingSessionResponse> GetByIdAsync(int sessionId)
        {
            var session = await shoppingSessionRepository.RetrieveAsync(sessionId);
            if (session.Deadline > DateTime.Now)
            {
                session.IsActive = true;
            }
            var store = await storeRepository.RetrieveAsync(session.StoreId);
            return new GetShoppingSessionResponse
            {
                IsActive = session.IsActive,
                CreatedAt = session.CreatedAt,
                CreatedByEmployeeId = session.CreatedByEmployeeId,
                Deadline = session.Deadline,
                ShoppingSessionId = session.ShoppingSessionId,
                StoreId = session.StoreId,
                StoreName = store.Name
            };
        }

        public async Task<GetEndedShoppingSessionsHistoryResponse> GetEndedShoppingSessionsHistory()
        {
            var shoppingSessions = new List<ShoppingSessionInfo>();

            await foreach (var session in shoppingSessionRepository.RetrieveCollectionAsync(new ShoppingSessionFilter()))
            {
                if (!session.IsActive)
                {
                    shoppingSessions.Add(new ShoppingSessionInfo
                    {
                        CreatedAt = session.CreatedAt,
                        CreatedByEmployeeId = session.CreatedByEmployeeId,
                        Deadline = session.Deadline,
                        ShoppingSessionId = session.ShoppingSessionId,
                        StoreId = session.StoreId,
                        IsActive = session.IsActive
                    });
                }
            }

            return new GetEndedShoppingSessionsHistoryResponse
            {
                Sessions = shoppingSessions,
                TotalCount = shoppingSessions.Count
            };
        }

    }
}
