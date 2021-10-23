using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FILEIDSMVC.Controllers
{
    public class DesignReviewController : Controller
    {
        // GET: DesignReview
        public ActionResult Index()
        {
            return View();
        }

        // GET: DesignReview/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DesignReview/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DesignReview/Create
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

        // GET: DesignReview/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DesignReview/Edit/5
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

        // GET: DesignReview/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DesignReview/Delete/5
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
