using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using DataLayer.Models;
using System.Collections.Generic;

namespace PresentationLayer.Controllers
{
    public class AuthController(EmployeeBusiness employeeBusiness) : Controller
    {
        private readonly EmployeeBusiness _employeeBusiness = employeeBusiness;

        public IActionResult Login()
        {
            return View(new Employee());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Employee employee)
        {
            if (employee.Id == 0 || string.IsNullOrEmpty(employee.Password))
            {
                ModelState.AddModelError(string.Empty, "Employee ID must be a number and password is required.");
                return View(employee);
            }

            Employee validatedEmployee = _employeeBusiness.ValidateEmployeeCredentials(employee.Id, employee.Password);

            if (validatedEmployee != null)
            {
                string fullName = $"{validatedEmployee.Name} {validatedEmployee.Surname}";

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, validatedEmployee.Id.ToString()),
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.Role, validatedEmployee.Role)
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Index", "Inventory");
            }

            ModelState.AddModelError(string.Empty, "Invalid Employee ID or password.");
            return View(employee);
        }

        public async Task<IActionResult> Skip()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Guest User"),
                new Claim(ClaimTypes.Role, "Guest")
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index", "Inventory");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction(nameof(Login));
        }


        public IActionResult AccessDenied()
        {
            TempData["ErrorMessage"] = "You do not have the required permissions to access the requested page.";
            return RedirectToAction("Index", "Inventory");
        }
    }
}