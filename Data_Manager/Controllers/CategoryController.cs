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
    public class CategoryController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Category.OrderByDescending(a => a.CategoryID).ToList());
            }

            return View(db.tbl_Category.OrderByDescending(a => a.CategoryID).Where(a => a.Org_Id == id).ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Category tbl_category)
        {
            if (ModelState.IsValid)
            {

                tbl_category.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_category.IsActive = "Y";
                tbl_category.IsDelete = "N";
                tbl_category.CreateDate = DateTime.Now;
                tbl_category.CreateBy = Session["name"].ToString();
                tbl_category.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Category.Add(tbl_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Category tbl_category)
        {
            if (ModelState.IsValid)
            {
                tbl_category.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_category.IsActive = "Y";
                tbl_category.IsDelete = "N";
                tbl_category.UpdateDate = DateTime.Now;
                tbl_category.UpdateBy = Session["name"].ToString();
                tbl_category.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Category tbl_category = db.tbl_Category.Find(id);
            db.tbl_Category.Remove(tbl_category);
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