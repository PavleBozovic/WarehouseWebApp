using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class EmployeeController(EmployeeBusiness employeeBusiness) : Controller
    {
        private readonly EmployeeBusiness _employeeBusiness = employeeBusiness;

        public IActionResult Index()
        {
            var employees = _employeeBusiness.GetAllEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = _employeeBusiness.InsertEmployee(employee);
                if (result)
                {
                    TempData["SuccessMessage"] = $"Employee '{employee.Name}' added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to add employee. Please, check for invalid inputs.");
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeeBusiness.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (employee.Id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = _employeeBusiness.UpdateEmployee(employee);

                if (result)
                {
                    TempData["SuccessMessage"] = $"Employee '{employee.Name}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to save changes. A database error occurred or employee details are invalid.");
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _employeeBusiness.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employeeToDelete = new Employee { Id = id };

            bool result = _employeeBusiness.DeleteEmployee(employeeToDelete);

            if (result)
            {
                TempData["SuccessMessage"] = "Employee deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting employee. The item may be linked to other data.";
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}