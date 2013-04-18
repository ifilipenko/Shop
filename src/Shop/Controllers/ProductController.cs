using System.Web.Mvc;
using Shop.Models;
using Shop.Services.Domain;
using Shop.Services.Domain.Commands;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

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
            }

            var id = _productService.SaveProduct(new ProductSaveCommand
                {
                    Id          = model.Id,
                    Name        = model.Name,
                    Category    = model.Category,
                    Description = model.Description,
                    Vendor      = model.Vendor
                });
            TempData["notice"] = "Товар успешно создан";
            return RedirectToAction("Edit", "Product", new {id = id});
        }
    }
}