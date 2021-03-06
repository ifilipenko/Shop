using System.Web.Mvc;
using Shop.Domain.Model;
using Shop.Domain.Models;

namespace Shop.Controllers
{   
    public class CategoryController : Controller
    {
		private readonly ICategoryRepository categoryRepository;

		public CategoryController(ICategoryRepository categoryRepository)
        {
			this.categoryRepository = categoryRepository;
        }

        //
        // GET: /Category/

        public ViewResult Index()
        {
            return View(categoryRepository.All);
        }

        //
        // GET: /Category/Details/5

        public ViewResult Details(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid) {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(categoryRepository.Find(id));
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                categoryRepository.InsertOrUpdate(category);
                categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(categoryRepository.Find(id));
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryRepository.Delete(id);
            categoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                categoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

