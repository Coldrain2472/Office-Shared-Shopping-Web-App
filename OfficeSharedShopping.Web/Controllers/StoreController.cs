using Microsoft.AspNetCore.Mvc;
using OfficeSharedShopping.Services.Interfaces.Store;
using OfficeSharedShopping.Web.Attributes;
using OfficeSharedShopping.Web.Models.Store;

namespace OfficeSharedShopping.Web.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IStoreService storeService;

        public StoreController(IStoreService _storeService)
        {
            storeService = _storeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stores = await storeService.GetAllAsync();
            var viewModel = new StoreListViewModel
            {
                Stores = stores.Stores.Select(s => new StoreViewModel
                {
                    StoreId = s.StoreId,
                    Name = s.Name
                })
                .ToList(),
                TotalCount = stores.TotalCount
            };

            return View(viewModel);
        }
    }
}
