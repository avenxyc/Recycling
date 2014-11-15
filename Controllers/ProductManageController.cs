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
                .Where(p => p.ProductName.StartsWith(term) ||
                            p.UPC.Contains(term))
                .Take(10)
                .Select(p => new
                {
                    label = p.ProductName
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
                // Save Image to server and URL to ProductImage model
                HttpPostedFileBase file = Request.Files["ProductImage"];
                var upc = collection["UPC"];
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType == "image/jpeg" ||
                        file.ContentType == "image/jpg" ||
                        file.ContentType == "image/png")
                    {
                        // extract only the fielname
                        var fileExtension = Path.GetExtension(file.FileName);
                        // store the file inside ~/App_Data/uploads folder
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Uploaded"), upc + fileExtension);
                        file.SaveAs(path);
                        var productImage = new ProductImage();
                        productImage.ImageUrl = path;
                    }

                }


                //int cnumber = Request.Form["cname"].Count();
                //Console.Write(cnumber);

                var newproduct = new ProductView();
                var constituentList = new List<Constituent> { };
                var productHasConstituents = new List<ProductHasConstituent> { };
                var locatedIn = new LocatedIn();

                string[] cnames = collection["cform[cname][]"].Split(',');
                string[] cpweights = collection["cform[pweight][]"].Split(',');
                string[] cType = collection["cform[Type][]"].Split(',');
                string[] cclassifications = collection["cform[classification][]"].Split(',');


                string [][] ccollection = {cnames, cpweights, cType, cclassifications};
                

                



                if (ModelState.IsValid)
                {
                    //db.Products.Add(product);
                    //db.SaveChanges();
                    newproduct.UPC = collection["UPC"];
                    newproduct.ProductName = collection["ProductName"];
                    newproduct.CompanyName = collection["CompanyName"];
                    newproduct.ParentCompany = collection["ParentCompany"];
                    newproduct.Weight = double.Parse(collection["weight"]);
                    newproduct.TotalWeight = double.Parse(collection["TotalWeight"]);
                    newproduct.NumberOfConstituent = uint.Parse(collection["NumberOfConstituent"]);
                    _names.Add(newproduct);
                }

                //return RedirectToAction("Index");


                return (Json(ccollection, JsonRequestBehavior.AllowGet));
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
