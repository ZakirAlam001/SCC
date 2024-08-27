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
    public class DailyDepositController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /DailyDeposit/

        public ActionResult Index()
        {
            return View(db.tbl_deposit_Amount.ToList());
        }

        //
        // GET: /DailyDeposit/Details/5

        public ActionResult Details(long id = 0)
        {
            tbl_deposit_Amount tbl_deposit_amount = db.tbl_deposit_Amount.Find(id);
            if (tbl_deposit_amount == null)
            {
                return HttpNotFound();
            }
            return View(tbl_deposit_amount);
        }

        //
        // GET: /DailyDeposit/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DailyDeposit/Create

        [HttpPost]
    //    [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_deposit_Amount tbl_deposit_amount)
        {
   //        if (ModelState.IsValid)
            {
                db.tbl_deposit_Amount.Add(tbl_deposit_amount);
                tbl_deposit_amount.IsDelete = "No";
                tbl_deposit_amount.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_deposit_amount.CreateBy = Session["name"].ToString();
                tbl_deposit_amount.CreateDate = DateTime.Now;
                tbl_deposit_amount.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_deposit_amount);
        }

        //
        // GET: /DailyDeposit/Edit/5

        public ActionResult Edit(long id = 0)
        {
            tbl_deposit_Amount tbl_deposit_amount = db.tbl_deposit_Amount.Find(id);
            if (tbl_deposit_amount == null)
            {
                return HttpNotFound();
            }
            return View(tbl_deposit_amount);
        }

        //
        // POST: /DailyDeposit/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_deposit_Amount tbl_deposit_amount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_deposit_amount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_deposit_amount);
        }

        //
        // GET: /DailyDeposit/Delete/5

        public ActionResult Delete(long id = 0)
        {
            tbl_deposit_Amount tbl_deposit_amount = db.tbl_deposit_Amount.Find(id);
            if (tbl_deposit_amount == null)
            {
                return HttpNotFound();
            }
            return View(tbl_deposit_amount);
        }

        //
        // POST: /DailyDeposit/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_deposit_Amount tbl_deposit_amount = db.tbl_deposit_Amount.Find(id);
            db.tbl_deposit_Amount.Remove(tbl_deposit_amount);
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