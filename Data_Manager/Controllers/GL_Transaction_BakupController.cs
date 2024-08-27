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
    public class GL_Transaction_BakupController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /GL_Transaction_Bakup/

        public ActionResult Index()
        {
            return View(db.tbl_Account_Mst_Transaction_Backup.ToList());
        }

        //
        // GET: /GL_Transaction_Bakup/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Account_Mst_Transaction_Backup tbl_account_mst_transaction_backup = db.tbl_Account_Mst_Transaction_Backup.Find(id);
            if (tbl_account_mst_transaction_backup == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account_mst_transaction_backup);
        }

        //
        // GET: /GL_Transaction_Bakup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GL_Transaction_Bakup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Account_Mst_Transaction_Backup tbl_account_mst_transaction_backup)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Account_Mst_Transaction_Backup.Add(tbl_account_mst_transaction_backup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_account_mst_transaction_backup);
        }

        //
        // GET: /GL_Transaction_Bakup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Account_Mst_Transaction_Backup tbl_account_mst_transaction_backup = db.tbl_Account_Mst_Transaction_Backup.Find(id);
            if (tbl_account_mst_transaction_backup == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account_mst_transaction_backup);
        }

        //
        // POST: /GL_Transaction_Bakup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Account_Mst_Transaction_Backup tbl_account_mst_transaction_backup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_account_mst_transaction_backup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_account_mst_transaction_backup);
        }

        //
        // GET: /GL_Transaction_Bakup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Account_Mst_Transaction_Backup tbl_account_mst_transaction_backup = db.tbl_Account_Mst_Transaction_Backup.Find(id);
            if (tbl_account_mst_transaction_backup == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account_mst_transaction_backup);
        }

        //
        // POST: /GL_Transaction_Bakup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Account_Mst_Transaction_Backup tbl_account_mst_transaction_backup = db.tbl_Account_Mst_Transaction_Backup.Find(id);
            db.tbl_Account_Mst_Transaction_Backup.Remove(tbl_account_mst_transaction_backup);
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