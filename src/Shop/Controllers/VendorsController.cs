﻿using System.Data;
using System.Linq;
using System.Web.Mvc;
using Shop.Domain.Model;
using Shop.Models;

namespace Shop.Controllers
{
    public class VendorsController : Controller
    {
        private ProductModelContext db = new ProductModelContext();

        //
        // GET: /Vendors/

        public ActionResult Index()
        {
            return View(db.Vendors.ToList());
        }

        //
        // GET: /Vendors/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vendors/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendorViewModel model)
        {
            if (ModelState.IsValid)
            {
                db.Vendors.Add(model.MapToDomainModel());
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Vendors/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor.MapToViewModel());
        }

        //
        // POST: /Vendors/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendorViewModel vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendor.MapToDomainModel()).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }

        //
        // GET: /Vendors/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendors/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = db.Vendors.Find(id);
            db.Vendors.Remove(vendor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}