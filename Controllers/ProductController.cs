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
using System.IO;

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
                .Where(p => p.ProductName.StartsWith(term) ||
                            p.UPC.Contains(term))
                .Take(10)
                .Select(p => new
                {
                    label = p.ProductName
                });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        // GET: Database
        public ActionResult Index(string searchTerm = null, int page = 1)
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
                           p.ProductName.Contains(searchTerm))
                  .OrderBy(p => p.ProductName)
                  .Select(p => p)
                  .ToPagedList(page, 10);

            var is_Ajax = Request.IsAjaxRequest();
            if (is_Ajax)
            {
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
        //[Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        
        [HttpPost]
        //[Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UPC,Name,CompanyName,ParentCompany,Weight,TotalWeight,Category,ProductImage")] Product product)
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

        public ActionResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Images/Uploaded"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

                Console.Write("Test test");

            }
            // after successfully uploading redirect the user
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
