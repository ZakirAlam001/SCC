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
    public class VendorController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Vendor/

        public ActionResult Index()
        {
           
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Vendor.ToList());
            }
           
            return View(db.tbl_Vendor.Where(a => a.Org_Id == id).ToList());
        }

        //
        // GET: /Vendor/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Vendor tbl_vendor = db.tbl_Vendor.Find(id);
            if (tbl_vendor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor);
        }

        //
        // GET: /Vendor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vendor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Vendor tbl_vendor)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Vendor.Add(tbl_vendor);
                tbl_vendor.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_vendor.UserID = Convert.ToInt32(Session["UserID"]);
               
                tbl_vendor.IsDelete = "N";
                tbl_vendor.CreateDate = DateTime.Now;
                tbl_vendor.CreateBy = Session["name"].ToString();
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_vendor);
        }

        //
        // GET: /Vendor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Vendor tbl_vendor = db.tbl_Vendor.Find(id);
            if (tbl_vendor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor);
        }

        //
        // POST: /Vendor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Vendor tbl_vendor)
        {
            if (ModelState.IsValid)
            {
                tbl_vendor.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_vendor.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_vendor.IsDelete = "N";
                tbl_vendor.UpdateDate = DateTime.Now;
                tbl_vendor.UpdateBy = Session["name"].ToString();
                db.Entry(tbl_vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_vendor);
        }

        //
        // GET: /Vendor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Vendor tbl_vendor = db.tbl_Vendor.Find(id);
            if (tbl_vendor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor);
        }

        //
        // POST: /Vendor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Vendor tbl_vendor = db.tbl_Vendor.Find(id);
            db.tbl_Vendor.Remove(tbl_vendor);
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