using Recycling.Models
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recycling.Controllers
{
    public class HomeController : Controller
    {
        RecyclingDb _db = new RecyclingDb();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Database()
        {
            var product = _db.Products.ToList();


            return View(product);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
               _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}