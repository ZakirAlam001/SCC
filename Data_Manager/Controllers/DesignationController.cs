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
    public class DesignationController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Designation/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Designation.ToList());
            }

            return View(db.tbl_Designation.Where(a => a.Org_Id == id).ToList());
           
        }

        //
        // GET: /Designation/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Designation tbl_designation = db.tbl_Designation.Find(id);
            if (tbl_designation == null)
            {
                return HttpNotFound();
            }
            return View(tbl_designation);
        }

        //
        // GET: /Designation/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Designation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Designation tbl_designation)
        {
            if (ModelState.IsValid)
            {
                tbl_designation.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Designation.Add(tbl_designation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_designation);
        }

        //
        // GET: /Designation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Designation tbl_designation = db.tbl_Designation.Find(id);
            if (tbl_designation == null)
            {
                return HttpNotFound();
            }
            return View(tbl_designation);
        }

        //
        // POST: /Designation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Designation tbl_designation)
        {
            if (ModelState.IsValid)
            {
                tbl_designation.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_designation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_designation);
        }

        //
        // GET: /Designation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Designation tbl_designation = db.tbl_Designation.Find(id);
            if (tbl_designation == null)
            {
                return HttpNotFound();
            }
            return View(tbl_designation);
        }

        //
        // POST: /Designation/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Designation tbl_designation = db.tbl_Designation.Find(id);
            db.tbl_Designation.Remove(tbl_designation);
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