using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Shop.Domain;
using Shop.Domain.Model;
using Shop.Domain.Repositories;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController : EntityFrameworkController<ProductModelContext>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWorkScope<ProductModelContext> _scope;

        public ProductController(IProductRepository productRepository, IUnitOfWorkScope<ProductModelContext> scope)
            : base(scope)
        {
            _productRepository = productRepository;
            _scope = scope;
        }

        public ActionResult Index(int? page)
        {
            var products = _productRepository.GetAll().ToPagedList(page ?? 1, 3);
            return View(products);
        }

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
                return HttpNotFound("Товар не найден");
            }

            ViewBag.notice = TempData["notice"];
            return View("CreateOrEdit", new EditProduct
            {
                Id          = id,
                Name        = product.Name,
                Category    = product.Category,
                Description = product.Description,
                Vendor      = product.Vendor
            });
        }

        [HttpPost]
        public ActionResult Save(EditProduct model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Product
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Category = model.Category,
                            Description = model.Description,
                            Vendor = model.Vendor
                        };
                    _productRepository.Save(product);

                    TempData["notice"] = model.IsEdit ? "Товар успешно изменен" : "Товар успешно добавлен";
                    return RedirectToAction("Edit", new {id = product.Id});
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View("CreateOrEdit", model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = _productRepository.FindById(id);
            if (product == null)
                return HttpNotFound("Товар не найден");

            _productRepository.Delete(product);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
