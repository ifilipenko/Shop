using System;
using System.Web.Mvc;
using Shop.Models;
using Shop.Services.Domain;
using Shop.Services.Domain.Dto;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateOrEdit", new EditProduct());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.notice = TempData["notice"];
            var model = new EditProduct
                {
                    Id          = id,
                    Name        = product.Name,
                    Category    = product.Category,
                    Description = product.Description,
                    Vendor      = product.Vendor
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
                    var product = new Product
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Category = model.Category,
                            Description = model.Description,
                            Vendor = model.Vendor
                        };
                    _productRepository.Save(product);
                    return RedirectToAction("Edit", "Product", new {id = product.Id});
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