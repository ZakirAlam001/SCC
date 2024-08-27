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
    public class ExpenseDetailController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /ExpenseDetail/

        public ActionResult Index()
        {

            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_ExpenseDetail.Include(t => t.tbl_MstExpense).ToList());
            }

            var tbl_expencedetail = db.tbl_ExpenseDetail.Include(t => t.tbl_MstExpense).Where(a => a.Org_Id == id);
            return View(tbl_expencedetail.ToList());


        }

        //
        // GET: /ExpenseDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_ExpenseDetail tbl_expencedetail = db.tbl_ExpenseDetail.Find(id);
            if (tbl_expencedetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_expencedetail);
        }

        //
        // GET: /ExpenseDetail/Create

        public ActionResult Create()
        {
            ViewBag.ExpMstID = new SelectList(db.tbl_MstExpense, "ExpMstID", "IsDelete");
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name");
            return View();
        }

        //
        // POST: /ExpenseDetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_ExpenseDetail tbl_expencedetail)
        {
            if (ModelState.IsValid)
            {
                tbl_expencedetail.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_ExpenseDetail.Add(tbl_expencedetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpMstID = new SelectList(db.tbl_MstExpense, "ExpMstID", "IsDelete", tbl_expencedetail.ExpMstID);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name");
            return View(tbl_expencedetail);
        }

        //
        // GET: /ExpenseDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_ExpenseDetail tbl_expencedetail = db.tbl_ExpenseDetail.Find(id);
            if (tbl_expencedetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpMstID = new SelectList(db.tbl_MstExpense, "ExpMstID", "IsDelete", tbl_expencedetail.ExpMstID);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name");
            return View(tbl_expencedetail);
        }

        //
        // POST: /ExpenseDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_ExpenseDetail tbl_expencedetail)
        {
            if (ModelState.IsValid)
            {
                tbl_expencedetail.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_expencedetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpMstID = new SelectList(db.tbl_MstExpense, "ExpMstID", "IsDelete", tbl_expencedetail.ExpMstID);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name");
            return View(tbl_expencedetail);
        }

        //
        // GET: /ExpenseDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_ExpenseDetail tbl_expencedetail = db.tbl_ExpenseDetail.Find(id);
            if (tbl_expencedetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_expencedetail);
        }

        //
        // POST: /ExpenseDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_ExpenseDetail tbl_expencedetail = db.tbl_ExpenseDetail.Find(id);
            db.tbl_ExpenseDetail.Remove(tbl_expencedetail);
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