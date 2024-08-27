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
    public class CommissionController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Commission/

        public ActionResult Index()
        {
            return View(db.tbl_Commission.ToList());
        }

        //
        // GET: /Commission/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Commission tbl_commission = db.tbl_Commission.Find(id);
            if (tbl_commission == null)
            {
                return HttpNotFound();
            }
            return View(tbl_commission);
        }

        //
        // GET: /Commission/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Commission/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Commission tbl_commission)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Commission.Add(tbl_commission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_commission);
        }

        //
        // GET: /Commission/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Commission tbl_commission = db.tbl_Commission.Find(id);
            if (tbl_commission == null)
            {
                return HttpNotFound();
            }
            return View(tbl_commission);
        }

        //
        // POST: /Commission/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Commission tbl_commission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_commission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_commission);
        }

        //
        // GET: /Commission/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Commission tbl_commission = db.tbl_Commission.Find(id);
            if (tbl_commission == null)
            {
                return HttpNotFound();
            }
            return View(tbl_commission);
        }

        //
        // POST: /Commission/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Commission tbl_commission = db.tbl_Commission.Find(id);
            db.tbl_Commission.Remove(tbl_commission);
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