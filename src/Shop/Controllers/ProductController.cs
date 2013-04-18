using System;
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
            var product = _productService.FindById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

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
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["notice"] = model.IsEditMode ? "Товар успешно изменен" : "Товар успешно создан";
                    var id = _productService.SaveProduct(new ProductSaveCommand
                    {
                        Id          = model.Id,
                        Name        = model.Name,
                        Category    = model.Category,
                        Description = model.Description,
                        Vendor      = model.Vendor
                    });
                    return RedirectToAction("Edit", "Product", new { id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View("CreateOrEdit", model);
        }
    }
}