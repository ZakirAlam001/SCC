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
    public class OrganizationController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Organization/

        public ActionResult Index()
        {
            return View(db.tbl_Orgcode.ToList());
        }

        //
        // GET: /Organization/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Orgcode tbl_orgcode = db.tbl_Orgcode.Find(id);
            if (tbl_orgcode == null)
            {
                return HttpNotFound();
            }
            return View(tbl_orgcode);
        }

        //
        // GET: /Organization/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Organization/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Orgcode tbl_orgcode)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Orgcode.Add(tbl_orgcode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_orgcode);
        }

        //
        // GET: /Organization/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Orgcode tbl_orgcode = db.tbl_Orgcode.Find(id);
            if (tbl_orgcode == null)
            {
                return HttpNotFound();
            }
            return View(tbl_orgcode);
        }

        //
        // POST: /Organization/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Orgcode tbl_orgcode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_orgcode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_orgcode);
        }

        //
        // GET: /Organization/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Orgcode tbl_orgcode = db.tbl_Orgcode.Find(id);
            if (tbl_orgcode == null)
            {
                return HttpNotFound();
            }
            return View(tbl_orgcode);
        }

        //
        // POST: /Organization/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Orgcode tbl_orgcode = db.tbl_Orgcode.Find(id);
            db.tbl_Orgcode.Remove(tbl_orgcode);
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