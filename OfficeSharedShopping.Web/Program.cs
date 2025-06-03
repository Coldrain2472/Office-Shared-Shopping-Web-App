using OfficeSharedShopping.Repository;
using OfficeSharedShopping.Repository.Implementations.Employee;
using OfficeSharedShopping.Repository.Implementations.Product;
using OfficeSharedShopping.Repository.Implementations.ProductCategory;
using OfficeSharedShopping.Repository.Implementations.SessionRequest;
using OfficeSharedShopping.Repository.Implementations.ShoppingSession;
using OfficeSharedShopping.Repository.Implementations.Store;
using OfficeSharedShopping.Repository.Interfaces;
using OfficeSharedShopping.Repository.Interfaces.Employee;
using OfficeSharedShopping.Repository.Interfaces.ProductCategory;
using OfficeSharedShopping.Repository.Interfaces.SessionRequest;
using OfficeSharedShopping.Repository.Interfaces.ShoppingSession;
using OfficeSharedShopping.Repository.Interfaces.Store;
using OfficeSharedShopping.Services.Implementations.Authentication;
using OfficeSharedShopping.Services.Implementations.Employee;
using OfficeSharedShopping.Services.Implementations.Product;
using OfficeSharedShopping.Services.Implementations.ProductCategory;
using OfficeSharedShopping.Services.Implementations.SessionRequest;
using OfficeSharedShopping.Services.Implementations.ShoppingSession;
using OfficeSharedShopping.Services.Implementations.Store;
using OfficeSharedShopping.Services.Interfaces.Authentication;
using OfficeSharedShopping.Services.Interfaces.Employee;
using OfficeSharedShopping.Services.Interfaces.Product;
using OfficeSharedShopping.Services.Interfaces.ProductCategory;
using OfficeSharedShopping.Services.Interfaces.SessionRequest;
using OfficeSharedShopping.Services.Interfaces.ShoppingSession;
using OfficeSharedShopping.Services.Interfaces.Store;

namespace OfficeSharedShopping.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<ISessionRequestService, SessionRequestService>();
            builder.Services.AddScoped<IShoppingSessionService, ShoppingSessionService>();

            // repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            builder.Services.AddScoped<ISessionRequestRepository, SessionRequestRepository>();
            builder.Services.AddScoped<IShoppingSessionRepository, ShoppingSessionRepository>();

            // connection to the db
            ConnectionFactory.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));

            // session properties
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
