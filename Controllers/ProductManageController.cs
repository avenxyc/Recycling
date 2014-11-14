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
    public class ProductManageController : Controller
    {
        private RecyclingDb db = new RecyclingDb();

        // Get: ProductMange/Autocomplete
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

        // Get: ProductManage
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
                           p.Name.Contains(searchTerm))
                  .OrderBy(p => p.Name)
                  .Select(p => p)
                  .ToPagedList(page, 10);

            var is_Ajax = Request.IsAjaxRequest();
            if (is_Ajax)
            {
                return PartialView("_Products", product);
            }

            return View(product);
        }

        // GET: ProductManage/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductView model = _names.Find(x => x.UPC == id);
            if (model == null)
            {
                //return View("NotFound");
                return HttpNotFound();
            }
            
            return View(model);
        }

        // GET: ProductManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductManage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var newproduct = new ProductView();
                var ConstituentList = new List<Constituent> {};
                var region = new Region();

                newproduct.UPC = collection["UPC"];
                newproduct.ProductName = collection["ProductName"];
                region.RegionName = collection["Region"];
                
                

                _names.Add(newproduct);

                //return RedirectToAction("Index");

                return(Json(_names, JsonRequestBehavior.AllowGet));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductManage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductManage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductManage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductManage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        static List<ProductView> _names = new List<ProductView>
        {
            new ProductView {
                UPC = "1",
                ProductName = "Cookies",
                NumberOfConstituent = 2
            },
            new ProductView {
                UPC = "2",
                ProductName = "Cookies1",
                NumberOfConstituent = 2
            },
            new ProductView {
                UPC = "3",
                ProductName = "Cookies2",
                NumberOfConstituent = 2
            },
        };

    }
}
