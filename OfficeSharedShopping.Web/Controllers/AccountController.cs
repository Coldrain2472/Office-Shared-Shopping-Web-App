﻿using Microsoft.AspNetCore.Mvc;
using OfficeSharedShopping.Services.Interfaces.Authentication;
using OfficeSharedShopping.Web.Models.Account;

namespace OfficeSharedShopping.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService authService;

        public AccountController(IAuthenticationService _authService)
        {
            authService = _authService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await authService.LoginAsync(new Services.DTOs.Authentication.LoginRequest
            {
                Email = model.Email,
                Password = model.Password
            });

            if (result.Success)
            {
                HttpContext.Session.SetInt32("UserId", result.EmployeeId.Value);
                HttpContext.Session.SetString("UserName", result.Name);

                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", nameof(HomeController));
            }


            ViewData["ErrorMessage"] = result.ErrorMessage ?? "Invalid email address or password";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
