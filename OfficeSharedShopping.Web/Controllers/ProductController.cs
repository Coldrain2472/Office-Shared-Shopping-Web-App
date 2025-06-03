using Microsoft.AspNetCore.Mvc;
using OfficeSharedShopping.Services.Interfaces.Product;
using OfficeSharedShopping.Services.Interfaces.ProductCategory;
using OfficeSharedShopping.Web.Attributes;
using OfficeSharedShopping.Web.Models.Product;

namespace OfficeSharedShopping.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly IProductCategoryService productCategoryService;

        public ProductController(IProductService _productService, IProductCategoryService _productCategoryService)
        {
            productService = _productService;
            productCategoryService = _productCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync();
            var productCategories = await productCategoryService.GetAllAsync();
            var categoryMap = productCategories.ProductCategories
                .ToDictionary(c => c.ProductCategoryId, c => c.Name);

            var viewModel = new ProductListViewModel
            {
                Products = products.Products.Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    CategoryName = categoryMap.ContainsKey(p.CategoryId) ? categoryMap[p.CategoryId] : "Unknown"
                })
                .ToList(),
                TotalCount = products.TotalCount
            };

            return View(viewModel);
        }
    }
}
