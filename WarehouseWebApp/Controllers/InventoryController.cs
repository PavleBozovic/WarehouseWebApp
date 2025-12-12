using Microsoft.AspNetCore.Mvc;
using BusinessLayer;

namespace PresentationLayer.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ItemBusiness _itemBusiness;

        public InventoryController(ItemBusiness itemBusiness)
        {
            this._itemBusiness = itemBusiness;
        }

        public IActionResult Index()
        {
            var items = _itemBusiness.GetAllItems();
            return View(items);
        }
    }
}
