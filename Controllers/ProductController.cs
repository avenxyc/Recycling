using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Recycling.Models;
using PagedList;

namespace Recycling.Controllers
{
    public class ProductController : Controller
    {
        private RecyclingDb db = new RecyclingDb();

        // Get: Product/Autocomplete
        public ActionResult Autocomplete(string term)
        {
            var model =
                db.Products
                .Where(p => p.Name.StartsWith(term) ||
                            p.UPC.Contains(term))
                .Take(10)
                .Select(p => new
                {
                    label = p.Name
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Database
        public ActionResult Index(string searchTerm = null)
        {
            // Conventional Syntax
            // var product =
            //    from p in db.Products
            //    // make searchTerm null to show all the data
            //    where searchTerm == null ||
            //          p.UPC.Contains(searchTerm) ||
            //          p.Name.Contains(searchTerm)
            //    orderby p.Name ascending
            //    select p;
            
            // Extention Systax
            var product =
                db.Products
                  .Where(p => searchTerm == null ||
                           p.UPC.Contains(searchTerm) ||
                           p.Name.Contains(searchTerm))
                  .OrderBy(p => p.Name)
                  .Select(p => p)
                  .Take(10);

            // product.ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                
                Content("This is a test");
                return PartialView("_Products", product);

            }
                return View(product);
        }


        // GET: Product/Details/
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                //return View("NotFound");
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UPC,Name,CompanyName,ParentCompany,Weight,TotalWeight,Category,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UPC,Name,CompanyName,ParentCompany,Weight,TotalWeight,Category,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
