using System.Web.Mvc;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private static int _lastProductId;

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateOrEdit", new EditProduct());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.notice = TempData["notice"];
            var model = new EditProduct
                {
                    Id = id,
                    Name = "Product " + id,
                    Category = "Category " + id,
                    Description = "Description " + id,
                    Vendor = "Vendor " + id
                };
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        public ActionResult Save(EditProduct model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateOrEdit", model);
            }

            if (model.IsEditMode)
            {
                TempData["notice"] = "Товар успешно изменен";
                return RedirectToAction("Edit", "Product", new {id = model.Id});
            }

            TempData["notice"] = "Товар успешно создан";
            return RedirectToAction("Edit", "Product", new {id = ++_lastProductId});
        }
    }
}