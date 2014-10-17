using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recycling.Models;

namespace Recycling.Controllers
{
    public class ConstituentController : Controller
    {
        RecyclingDb db = new RecyclingDb();

        // GET: Constituent
        public ActionResult Index()
        {
            return View();
        }

        // GET: Constituent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Constituent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Constituent/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Constituent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Constituent/Edit/5
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

        // GET: Constituent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Constituent/Delete/5
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
    }
}
