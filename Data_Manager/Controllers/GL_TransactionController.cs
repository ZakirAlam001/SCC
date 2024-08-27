using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class GL_TransactionController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /GL_Transaction/

        public ActionResult Index(int EmpID = 0, string fromdate = "", string todate = "")
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name");
            }
            else
            {
                ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a=>a.Org_Id==id), "EmpID", "Name");
            }
           
            
            if (id == 1)
            {
                if (EmpID > 0)
                {
                    var list = db.tbl_Account_Mst_Transaction.Where(a => a.IsDelete == "No" && a.EmpID == EmpID).OrderByDescending(a=>a.Acc_Trans_ID).ToList();
                   
                    return View(list);
                }
                return View(db.tbl_Account_Mst_Transaction.Where(a => a.IsDelete == "No" && a.EmpID != 3).OrderByDescending(a=>a.Acc_Trans_ID).Take(30).ToList());
            }
           
            if (EmpID > 0)
            {
                return View(db.tbl_Account_Mst_Transaction.Where(a => a.IsDelete == "No" && a.EmpID == EmpID && a.Org_Id == id).OrderByDescending(a => a.Acc_Trans_ID).ToList());
            }
            return View(db.tbl_Account_Mst_Transaction.Where(a => a.IsDelete == "No" && a.Org_Id == id).OrderByDescending(a => a.Acc_Trans_ID).Take(30).ToList());
        }

        //
        // GET: /GL_Transaction/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Account_Mst_Transaction tbl_account_mst_transaction = db.tbl_Account_Mst_Transaction.Find(id);
            if (tbl_account_mst_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account_mst_transaction);
        }

        //
        // GET: /GL_Transaction/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GL_Transaction/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Account_Mst_Transaction tbl_account_mst_transaction)
        {
            if (ModelState.IsValid)
            {
                tbl_account_mst_transaction.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Account_Mst_Transaction.Add(tbl_account_mst_transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_account_mst_transaction);
        }

        //
        // GET: /GL_Transaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Account_Mst_Transaction tbl_account_mst_transaction = db.tbl_Account_Mst_Transaction.Find(id);
            if (tbl_account_mst_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account_mst_transaction);
        }

        //
        // POST: /GL_Transaction/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Account_Mst_Transaction tbl_account_mst_transaction)
        {
            if (ModelState.IsValid)
            {
                tbl_account_mst_transaction.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_account_mst_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_account_mst_transaction);
        }

        //
        // GET: /GL_Transaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Account_Mst_Transaction tbl_account_mst_transaction = db.tbl_Account_Mst_Transaction.Find(id);
            if (tbl_account_mst_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tbl_account_mst_transaction);
        }

        //
        // POST: /GL_Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Account_Mst_Transaction tbl_account_mst_transaction = db.tbl_Account_Mst_Transaction.Find(id);
            db.tbl_Account_Mst_Transaction.Remove(tbl_account_mst_transaction);
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