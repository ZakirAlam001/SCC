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
    public class BankAccountController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /BankAccount/

        public ActionResult Index()
        {

            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
              //  var tbl_account = db.tbl_Account.Include(t => t.tbl_Bank);
                return View(db.tbl_Account.Include(t => t.tbl_Bank).ToList());
            }

            var tbl_account = db.tbl_Account.Include(t => t.tbl_Bank).Where(a=>a.Org_Id == id);
            return View(tbl_account.ToList());
        }

        //
        // GET: /BankAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Account tbl_account = db.tbl_Account.Find(id);
            if (tbl_account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account);
        }

        //
        // GET: /BankAccount/Create

        public ActionResult Create()
        {
            ViewBag.BankId = new SelectList(db.tbl_Bank, "BankId", "Name");
            return View();
        }

        //
        // POST: /BankAccount/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Account tbl_account)
        {
            if (ModelState.IsValid)
            {
                tbl_account.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Account.Add(tbl_account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.tbl_Bank, "BankId", "Name",tbl_account.BankId);

            return View(tbl_account);
        }

        //
        // GET: /BankAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Account tbl_account = db.tbl_Account.Find(id);
            if (tbl_account == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankId = new SelectList(db.tbl_Bank.Where(a => a.isDelete == "N"), "BankId", "Name", tbl_account.BankId);
            return View(tbl_account);
        }

        //
        // POST: /BankAccount/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Account tbl_account)
        {
            if (ModelState.IsValid)
            {
                tbl_account.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankId = new SelectList(db.tbl_Bank.Where(a => a.isDelete == "N"), "BankId", "Name", tbl_account.BankId);
            return View(tbl_account);
        }

        //
        // GET: /BankAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Account tbl_account = db.tbl_Account.Find(id);
            if (tbl_account == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account);
        }

        //
        // POST: /BankAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Account tbl_account = db.tbl_Account.Find(id);
            db.tbl_Account.Remove(tbl_account);
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