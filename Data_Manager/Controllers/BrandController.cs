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
    public class BrandController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Brand/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Brand.OrderByDescending(a => a.BrandID).ToList());
            }

            return View(db.tbl_Brand.OrderByDescending(a => a.BrandID).Where(a => a.Org_Id == id).ToList());
        }

        //
        // GET: /Brand/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            if (tbl_brand == null)
            {
                return HttpNotFound();
            }
            return View(tbl_brand);
        }

        //
        // GET: /Brand/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Brand/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Brand tbl_brand)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Brand.Add(tbl_brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_brand);
        }

        //
        // GET: /Brand/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            if (tbl_brand == null)
            {
                return HttpNotFound();
            }
            return View(tbl_brand);
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Brand tbl_brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_brand);
        }

        //
        // GET: /Brand/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            if (tbl_brand == null)
            {
                return HttpNotFound();
            }
            return View(tbl_brand);
        }

        //
        // POST: /Brand/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Brand tbl_brand = db.tbl_Brand.Find(id);
            db.tbl_Brand.Remove(tbl_brand);
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