﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class Loan_Transaction_BackupController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Loan_Transaction_Backup/

        public ActionResult Index()
        {
            return View(db.tbl_Loan_Transaction_Backup.ToList());
        }

        //
        // GET: /Loan_Transaction_Backup/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Loan_Transaction_Backup tbl_loan_transaction_backup = db.tbl_Loan_Transaction_Backup.Find(id);
            if (tbl_loan_transaction_backup == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loan_transaction_backup);
        }

        //
        // GET: /Loan_Transaction_Backup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Loan_Transaction_Backup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Loan_Transaction_Backup tbl_loan_transaction_backup)
        {
            if (ModelState.IsValid)
            {
                tbl_loan_transaction_backup.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Loan_Transaction_Backup.Add(tbl_loan_transaction_backup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_loan_transaction_backup);
        }

        //
        // GET: /Loan_Transaction_Backup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Loan_Transaction_Backup tbl_loan_transaction_backup = db.tbl_Loan_Transaction_Backup.Find(id);
            if (tbl_loan_transaction_backup == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loan_transaction_backup);
        }

        //
        // POST: /Loan_Transaction_Backup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Loan_Transaction_Backup tbl_loan_transaction_backup)
        {
            if (ModelState.IsValid)
            {
                tbl_loan_transaction_backup.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_loan_transaction_backup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_loan_transaction_backup);
        }

        //
        // GET: /Loan_Transaction_Backup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Loan_Transaction_Backup tbl_loan_transaction_backup = db.tbl_Loan_Transaction_Backup.Find(id);
            if (tbl_loan_transaction_backup == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loan_transaction_backup);
        }

        //
        // POST: /Loan_Transaction_Backup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Loan_Transaction_Backup tbl_loan_transaction_backup = db.tbl_Loan_Transaction_Backup.Find(id);
            db.tbl_Loan_Transaction_Backup.Remove(tbl_loan_transaction_backup);
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