using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly ItemBusiness _itemBusiness;

        public InventoryController(ItemBusiness itemBusiness)
        {
            this._itemBusiness = itemBusiness;
        }

        private bool IsGuest()
        {
            return (User?.Identity?.IsAuthenticated ?? false) && User.IsInRole("Guest");
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var items = _itemBusiness.GetAllItems();
            return View(items);
        }

        public IActionResult Create()
        {
            if (IsGuest())
            {
                TempData["GuestMessage"] = "You must log in as an employee to create new inventory items.";
                HttpContext.SignOutAsync("CookieAuth");
                return RedirectToAction("Login", "Auth");
            }

            return View(new DataLayer.Models.Item());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DataLayer.Models.Item item)
        {
            if (IsGuest())
            {
                TempData["GuestMessage"] = "You must log in as an employee to create new inventory items.";
                HttpContext.SignOutAsync("CookieAuth");
                return RedirectToAction("Login", "Auth");
            }

            if (ModelState.IsValid)
            {
                var result = _itemBusiness.InsertItem(item);
                if (result)
                {
                    TempData["SuccessMessage"] = $"Item '{item.Name}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to add item. Please check the quantity and other details.");
            }
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            if (IsGuest())
            {
                TempData["GuestMessage"] = "You must log in as an employee to edit inventory items.";
                HttpContext.SignOutAsync("CookieAuth");
                return RedirectToAction("Login", "Auth");
            }

            var item = _itemBusiness.GetItemById(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DataLayer.Models.Item item)
        {
            if (IsGuest())
            {
                TempData["GuestMessage"] = "You must log in as an employee to edit inventory items.";
                HttpContext.SignOutAsync("CookieAuth");
                return RedirectToAction("Login", "Auth");
            }

            if (item.Id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = _itemBusiness.UpdateItem(item);

                if (result)
                {
                    TempData["SuccessMessage"] = $"Item '{item.Name}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to save changes. A database error occurred or item details are invalid.");
            }
            return View(item);
        }

        public IActionResult Delete(int id)
        {
            if (IsGuest())
            {
                TempData["GuestMessage"] = "You must log in as an employee to delete inventory items.";
                HttpContext.SignOutAsync("CookieAuth");
                return RedirectToAction("Login", "Auth");
            }

            var item = _itemBusiness.GetItemById(id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (IsGuest())
            {
                TempData["GuestMessage"] = "You must log in as an employee to delete inventory items.";
                HttpContext.SignOutAsync("CookieAuth");
                return RedirectToAction("Login", "Auth");
            }

            DataLayer.Models.Item itemToDelete = new DataLayer.Models.Item { Id = id };

            bool result = _itemBusiness.DeleteItem(itemToDelete);

            if (result)
            {
                TempData["SuccessMessage"] = "Item deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Error deleting item. It may be linked to active orders.";
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}