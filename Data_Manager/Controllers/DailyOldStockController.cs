using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class DailyOldStockController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /DailyOldStock/

        public ActionResult Index()
        {
            return View(db.tbl_oldstockinout.ToList());
        }

        //
        // GET: /DailyOldStock/Details/5

        public ActionResult Details(long id = 0)
        {
            tbl_oldstockinout tbl_oldstockinout = db.tbl_oldstockinout.Find(id);
            if (tbl_oldstockinout == null)
            {
                return HttpNotFound();
            }
            return View(tbl_oldstockinout);
        }

        //
        // GET: /DailyOldStock/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DailyOldStock/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_oldstockinout tbl_oldstockinout)
        {
            if (ModelState.IsValid)
            {
                db.tbl_oldstockinout.Add(tbl_oldstockinout);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_oldstockinout);
        }

        //
        // GET: /DailyOldStock/Edit/5

        public ActionResult Edit(long id = 0)
        {
            tbl_oldstockinout tbl_oldstockinout = db.tbl_oldstockinout.Find(id);
            if (tbl_oldstockinout == null)
            {
                return HttpNotFound();
            }
            return View(tbl_oldstockinout);
        }

        //
        // POST: /DailyOldStock/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_oldstockinout tbl_oldstockinout)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_oldstockinout).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_oldstockinout);
        }

        //
        // GET: /DailyOldStock/Delete/5

        public ActionResult Delete(long id = 0)
        {
            tbl_oldstockinout tbl_oldstockinout = db.tbl_oldstockinout.Find(id);
            if (tbl_oldstockinout == null)
            {
                return HttpNotFound();
            }
            return View(tbl_oldstockinout);
        }

        //
        // POST: /DailyOldStock/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_oldstockinout tbl_oldstockinout = db.tbl_oldstockinout.Find(id);
            db.tbl_oldstockinout.Remove(tbl_oldstockinout);
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