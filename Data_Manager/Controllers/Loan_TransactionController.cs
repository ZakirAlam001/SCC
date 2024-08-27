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
    public class Loan_TransactionController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Loan_Transaction/

        
        public ActionResult Index(int LoanPersonID = 0)
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            
            if (id == 1)
            { ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson, "LoanPersonID", "Name");
            }else{
                ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson.Where(a=>a.Org_Id==id), "LoanPersonID", "Name");
            }

            
            if (id == 1)
            {
                if (LoanPersonID > 0)
                {
                    var tbl_loan_transactions = db.tbl_Loan_Transactions.Include(t => t.tbl_LoanPerson).Where(a => a.IsDelete == "No" && a.LoanPersonID == LoanPersonID).OrderByDescending(a => a.LoanID);
                    return View(tbl_loan_transactions.ToList());

                }

                var tbl_loan_transactions1 = db.tbl_Loan_Transactions.Include(t => t.tbl_LoanPerson).Where(a => a.IsDelete == "No").OrderByDescending(a => a.LoanID).Take(500);
                return View(tbl_loan_transactions1.ToList());
            }



            if (LoanPersonID > 0)
            {
                var tbl_loan_transactions = db.tbl_Loan_Transactions.Include(t => t.tbl_LoanPerson).Where(a => a.IsDelete == "No" && a.LoanPersonID == LoanPersonID && a.Org_Id== id).OrderByDescending(a => a.LoanID);
                return View(tbl_loan_transactions.ToList());
               
            }

            var tbl_loan_transactions2 = db.tbl_Loan_Transactions.Include(t => t.tbl_LoanPerson).Where(a => a.IsDelete == "No" && a.Org_Id== id).OrderByDescending(a => a.LoanID).Take(500);
            return View(tbl_loan_transactions2.ToList());
        }

        //
        // GET: /Loan_Transaction/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Loan_Transactions tbl_loan_transactions = db.tbl_Loan_Transactions.Find(id);
            if (tbl_loan_transactions == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loan_transactions);
        }

        //
        // GET: /Loan_Transaction/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson, "LoanPersonID", "Name");
            }
            else
            {
                ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson.Where(a => a.Org_Id == id), "LoanPersonID", "Name");
            }
            return View();
        }

        //
        // POST: /Loan_Transaction/Create

        [HttpPost]
        public ActionResult Create(tbl_Loan_Transactions tbl_loan_transactions)
        {
            if (ModelState.IsValid)
            {
                tbl_loan_transactions.ActiveBalance = true;
                var lastbalance = db.tbl_Loan_Transactions.Where(a => a.LoanPersonID == tbl_loan_transactions.LoanPersonID && a.ActiveBalance == true).OrderByDescending(a => a.CreateDate).FirstOrDefault();

                if (tbl_loan_transactions.Payment_Type == "CR")
                {
                    tbl_loan_transactions.CR_Amount = tbl_loan_transactions.Amount;
                    if (lastbalance == null)
                    {
                        tbl_loan_transactions.LastBalance = 0;
                        tbl_loan_transactions.Balance = tbl_loan_transactions.Amount;
                    }
                    else
                    {
                        tbl_loan_transactions.Balance = lastbalance.Balance - tbl_loan_transactions.Amount;
                        tbl_loan_transactions.LastBalance = lastbalance.Balance;
                    }

                }
                else if (tbl_loan_transactions.Payment_Type == "DR")
                {
                    tbl_loan_transactions.DR_Amount = tbl_loan_transactions.Amount;
                    if (lastbalance == null)
                    {
                        tbl_loan_transactions.LastBalance = 0;
                        tbl_loan_transactions.Balance = -tbl_loan_transactions.Amount;
                    }
                    else
                    {
                        tbl_loan_transactions.Balance = lastbalance.Balance - tbl_loan_transactions.Amount;
                        tbl_loan_transactions.LastBalance = lastbalance.Balance;
                    }

                }
                
                tbl_loan_transactions.CreateDate = DateTime.Now;
                tbl_loan_transactions.IsDelete = "No";
                tbl_loan_transactions.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_loan_transactions.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_loan_transactions.CreateDate = DateTime.Now;
                tbl_loan_transactions.CreateBy = Session["name"].ToString();
                db.tbl_Loan_Transactions.Add(tbl_loan_transactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson, "LoanPersonID", "Name", tbl_loan_transactions.LoanPersonID);
            return View(tbl_loan_transactions);
        }

        //
        // GET: /Loan_Transaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            int idd = Convert.ToInt32(Session["Org_Code"]);
            tbl_Loan_Transactions tbl_loan_transactions = db.tbl_Loan_Transactions.Find(id);
            if (tbl_loan_transactions == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson.Where(a=>a.Org_Id==idd), "LoanPersonID", "Name", tbl_loan_transactions.LoanPersonID);
            return View(tbl_loan_transactions);
        }

        //
        // POST: /Loan_Transaction/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Loan_Transactions tbl_loan_transactions)
        {
            if (ModelState.IsValid)
            {
                tbl_loan_transactions.UpdateDate = DateTime.Now;
                //tbl_loan_transactions.IsDelete = "No";
                tbl_loan_transactions.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_loan_transactions).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson, "LoanPersonID", "Name", tbl_loan_transactions.LoanPersonID);
            return View(tbl_loan_transactions);
        }

        //
        // GET: /Loan_Transaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Loan_Transactions tbl_loan_transactions = db.tbl_Loan_Transactions.Find(id);
            if (tbl_loan_transactions == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loan_transactions);
        }

        //
        // POST: /Loan_Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Loan_Transactions tbl_loan_transactions = db.tbl_Loan_Transactions.Find(id);
            db.tbl_Loan_Transactions.Remove(tbl_loan_transactions);
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