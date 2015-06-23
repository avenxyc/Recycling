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

        // GET: ProductManage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductManage/Create(FormCollection collection
        [HttpPost]
        public ActionResult Create([Bind(Include = "UPC,ProductName,CompanyName,ParentCompany,Weight,TotalWeight,Category")] Product product,
                                   FormCollection collection)
        {
            try
            {
                // Save Image to server and URL to ProductImage model
                HttpPostedFileBase file = Request.Files["ProductImage"];
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
                        var path = Path.Combine(Server.MapPath("~/Content/Images/Uploaded"), product.UPC + fileExtension);
                        file.SaveAs(path);
                        var productImage = new ProductImage();
                        productImage.ImageUrl = path;
                    }

                }


                // Get string of contituents and save them to string array
                string[] cnames = collection["cform[cname][]"].Split(',');
                string[] cpweights = collection["cform[pweight][]"].Split(',');
                string[] cType = collection["cform[Type][]"].Split(',');
                string[] cclassifications = collection["cform[classification][]"].Split(',');
                // Get the number of constituents
                int cnumber = cnames.Length;

                if (ModelState.IsValid)
                {
                    // Add product to database
                    db.Products.Add(product);
                    db.Regions.Add(new Region
                    {
                        RegionName = collection["Region"]
                    });
                    // Save new constituents to the list 
                    for (int i = 0; i < cnumber; i++)
                    {
                        db.Constituents.Add(
                        new Constituent
                            {
                                ConstituentName = cnames[i],
                                Type = cType[i]
                            });

                        db.ProductHasConstituents.Add(
                            new ProductHasConstituent{
                                UPC = product.UPC,
                                ConstituentName = cnames[i],
                                PartWeight = double.Parse(cpweights[i])
                            });
                        db.LocatedIns.Add(
                            new LocatedIn{
                                ConstituentName = cnames[i],
                                RegionName = collection["Region"],
                                Recyclability = cclassifications[i],
                            });
                    }

                    return RedirectToAction("Detail", new { id = product.UPC });
                }
                 return RedirectToAction("Index");



               // return (Json(product, JsonRequestBehavior.AllowGet));
            }
            catch
            {

                return HttpNotFound();
            }
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

            return View(sampleData);
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


        // POST: ProductManage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = new Product{ UPC = id.ToString()};
                db.Products.Remove(product);

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

        static ProductView sampleData = new ProductView
        {
            UPC = "12345678901",
            ProductName = "Oreo",
            CompanyName = "Kraft",
            ParentCompany = "Not Applicable",
            Weight = 500,
            TotalWeight = 550,
            Region = "HRM",
            Category = "Cookies",
            ConstituentName = "Paperbox",
            PartWeight = 50,
            Recyclability = "Blue Bag I",
            Type = "Paper",
            ImageURL = "~/Content/Images/Uploaded/2222.jpg"
        };

    }
}
