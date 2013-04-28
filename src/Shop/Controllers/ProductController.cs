using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using Shop.Domain.Model;
using Shop.Domain.Models;
using Shop.Domain.Repositories;
using Shop.EntityFramework;
using Shop.Models;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductModelContext _productModelContext;
        private readonly IUnitOfWorkScope _unitOfWorkScope;

        public ProductController(IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IProductModelContext productModelContext,
            IUnitOfWorkScope unitOfWorkScope)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productModelContext = productModelContext;
            _unitOfWorkScope = unitOfWorkScope;
        }

        public ActionResult Index(int? page)
        {
            var products = _productRepository.GetAll(x => x.Category, x => x.Vendor)
                                             .ToPagedList(page ?? 1, 3);
            return View(products);
        }

        public ActionResult Create()
        {
            return View("CreateOrEdit", new EditProduct
            {
                Categories = _categoryRepository.All.ToList(),
                Vendors = _productModelContext.Vendors.ToList()
            });
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
                CategoryId  = product.Category.Id,
                Description = product.Description,
                VendorId    = product.Vendor.Id,
                Categories  = _categoryRepository.All.ToList(),
                Vendors     = _productModelContext.Vendors.ToList()
            });
        }

        [HttpPost]
        public ActionResult Save(EditProduct model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vendor   = _productModelContext.Vendors.FirstOrDefault(x => x.Id == model.VendorId);
                    var category = _categoryRepository.Find(model.CategoryId ?? 0);
                    
                    var product = new Product
                        {
                            Id = model.Id,
                            Name = model.Name,
                            Category = category,
                            Description = model.Description,
                            Vendor = vendor
                        };
                    _productRepository.Save(product);
                    _unitOfWorkScope.Get().SubmitChanges();

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

        protected override void Dispose(bool disposing)
        {
            _categoryRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
