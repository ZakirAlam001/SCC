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
    public class BankController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Bank/

        public ActionResult Index()
        {
            //int id = Convert.ToInt32(Session["Org_Code"]);
            //if (id == 1)
            //{
            //    return View(db.tbl_Bank.ToList());
            //}
            return View(db.tbl_Bank.ToList());  
        }

        //
        // GET: /Bank/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Bank tbl_bank = db.tbl_Bank.Find(id);
            if (tbl_bank == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bank);
        }

        //
        // GET: /Bank/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bank/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Bank tbl_bank)
        {
            if (ModelState.IsValid)
            {
                tbl_bank.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Bank.Add(tbl_bank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_bank);
        }

        //
        // GET: /Bank/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Bank tbl_bank = db.tbl_Bank.Find(id);
            if (tbl_bank == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bank);
        }

        //
        // POST: /Bank/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Bank tbl_bank)
        {
            if (ModelState.IsValid)
            {
                tbl_bank.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_bank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_bank);
        }

        //
        // GET: /Bank/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Bank tbl_bank = db.tbl_Bank.Find(id);
            if (tbl_bank == null)
            {
                return HttpNotFound();
            }
            return View(tbl_bank);
        }

        //
        // POST: /Bank/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Bank tbl_bank = db.tbl_Bank.Find(id);
            db.tbl_Bank.Remove(tbl_bank);
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