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
    public class SalesTypeController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /SalesType/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_SalesTypeSetupForm.ToList());
            }
            return View(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id).OrderByDescending(a=>a.SaleTypeID).ToList());

        }

        //
        // GET: /SalesType/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_SalesTypeSetupForm tbl_salestypesetupform = db.tbl_SalesTypeSetupForm.Find(id);
            if (tbl_salestypesetupform == null)
            {
                return HttpNotFound();
            }
            return View(tbl_salestypesetupform);
        }

        //
        // GET: /SalesType/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.CategoryID = new SelectList(db.tbl_Category.Where(a => a.Org_Id == id), "CategoryID", "Name" );
            ViewBag.BrandID = new SelectList(db.tbl_Brand.Where(a => a.Org_Id == id), "BrandID", "Name");
          
            return View();
        }

        //
        // POST: /SalesType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_SalesTypeSetupForm tbl_salestypesetupform, string CategoryName, string BrandName)
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.CategoryID = new SelectList(db.tbl_Category.Where(a=>a.Org_Id == id), "CategoryID", "Name", tbl_salestypesetupform.CategoryID);
            ViewBag.BrandID = new SelectList(db.tbl_Brand.Where(a => a.Org_Id == id), "BrandID", "Name", tbl_salestypesetupform.BrandID);
          


            if (ModelState.IsValid)
            {
                tbl_salestypesetupform.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_SalesTypeSetupForm.Add(tbl_salestypesetupform);
                db.SaveChanges();
                ViewBag.msg = "Success";
                return RedirectToAction("Index");
            }
            
            return View(tbl_salestypesetupform);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CategoryName(string CategoryName)
        {
            tbl_Category tbl = new tbl_Category();
            if (ModelState.IsValid)
            {
                tbl.Name = CategoryName;
                tbl.UserID = Convert.ToInt32(Session["UserID"]);
                tbl.IsActive = "Y";
                tbl.IsDelete = "N";
                tbl.CreateDate = DateTime.Now;
                tbl.CreateBy = Session["name"].ToString();
                tbl.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Category.Add(tbl);
                db.SaveChanges();
                ViewBag.msg = "Success";
                return RedirectToAction("Create");
            }

            return View("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandName(string BrandName)
        {
            tbl_Brand tbl = new tbl_Brand();
            if (ModelState.IsValid)
            {
                tbl.Name = BrandName;
                tbl.UserID = Convert.ToInt32(Session["UserID"]);
                tbl.IsActive = "Y";
                tbl.IsDelete = "N";
                tbl.CreateDate = DateTime.Now;
                tbl.CreateBy = Session["name"].ToString();
                tbl.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Brand.Add(tbl);
                db.SaveChanges();
                ViewBag.msg = "Success";
                return RedirectToAction("Create");
            }
            return View("Create");
        }
        
        // GET: /SalesType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.CategoryID = new SelectList(db.tbl_Category, "CategoryID", "Name");
            ViewBag.BrandID = new SelectList(db.tbl_Brand, "BrandID", "Name");
            tbl_SalesTypeSetupForm tbl_salestypesetupform = db.tbl_SalesTypeSetupForm.Find(id);
            if (tbl_salestypesetupform == null)
            {
                return HttpNotFound();
            }
            return View(tbl_salestypesetupform);
        }

        //
        // POST: /SalesType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_SalesTypeSetupForm tbl_salestypesetupform)
        {
            ViewBag.CategoryID = new SelectList(db.tbl_Category, "CategoryID", "Name", tbl_salestypesetupform.CategoryID);
            ViewBag.BrandID = new SelectList(db.tbl_Brand, "BrandID", "Name", tbl_salestypesetupform.BrandID);
            if (ModelState.IsValid)
            {
                tbl_salestypesetupform.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_salestypesetupform).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_salestypesetupform);
        }

        //
        // GET: /SalesType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_SalesTypeSetupForm tbl_salestypesetupform = db.tbl_SalesTypeSetupForm.Find(id);
            if (tbl_salestypesetupform == null)
            {
                return HttpNotFound();
            }
            return View(tbl_salestypesetupform);
        }

        //
        // POST: /SalesType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_SalesTypeSetupForm tbl_salestypesetupform = db.tbl_SalesTypeSetupForm.Find(id);
            db.tbl_SalesTypeSetupForm.Remove(tbl_salestypesetupform);
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