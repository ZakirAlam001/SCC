using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class PaymentsController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Payments/

        public ActionResult Index(int EmpID=0)
        {
           
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name");
            if (id == 1)
            {
                if (EmpID > 0)
                {
                    return View(db.tbl_Payment_Rcv.Include(t => t.tbl_Employee).Where(a => a.EmpID == EmpID).OrderByDescending(a => a.Payment_ID).ToList());
                }
                return View(db.tbl_Payment_Rcv.Include(t => t.tbl_Employee).OrderByDescending(a => a.Payment_ID).Take(1000).ToList());
            }
            if (EmpID > 0)
            {
                var tbl_payment_rcv1 = db.tbl_Payment_Rcv.Include(t => t.tbl_Employee).Where(a => a.Org_Id == id && a.EmpID==EmpID);
                return View(tbl_payment_rcv1.OrderByDescending(a => a.Payment_ID).ToList());
            }
            var tbl_payment_rcv = db.tbl_Payment_Rcv.Include(t => t.tbl_Employee).Where(a => a.Org_Id == id);
            return View(tbl_payment_rcv.OrderByDescending(a=>a.Payment_ID).Take(50).ToList());
        }

        //
        // GET: /Payments/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Payment_Rcv tbl_payment_rcv = db.tbl_Payment_Rcv.Find(id);
            if (tbl_payment_rcv == null)
            {
                return HttpNotFound();
            }
            return View(tbl_payment_rcv);
        }

        //
        // GET: /Payments/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a=>a.Org_Id==id), "EmpID", "Name");
            return View();
        }

        //
        // POST: /Payments/Create

        [HttpPost]
    //    [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Payment_Rcv tbl_payment_rcv, string invostatus)
        {
            if (ModelState.IsValid)
            {
                using (TransactionScope tranScope = new TransactionScope())
                using (Entities_Data db = new Entities_Data())
                {
                    try
                    {
                        db.tbl_Payment_Rcv.Add(tbl_payment_rcv);
                        tbl_payment_rcv.UserID = Convert.ToInt32(Session["UserID"]);
                        tbl_payment_rcv.CreateBy = Session["name"].ToString();
                        tbl_payment_rcv.CreateDate = DateTime.Now;
                        tbl_payment_rcv.IsDelete = "No";
                        tbl_payment_rcv.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                        db.SaveChanges();

                        // int msid = Convert.ToInt32(tbl_payment_rcv.);

                        tbl_Account_Mst_Transaction acc = new tbl_Account_Mst_Transaction();
                        acc.Particular = tbl_payment_rcv.Description;

                        acc.Payment_ID = tbl_payment_rcv.Payment_ID;

                        acc.EmpID = tbl_payment_rcv.EmpID;
                        acc.DR_Amount = tbl_payment_rcv.Amount;
                        acc.ActiveBalance = true;
                        var lastbalance = db.tbl_Account_Mst_Transaction.Where(a => a.EmpID == tbl_payment_rcv.EmpID && a.ActiveBalance == true).OrderByDescending(a => a.CreateDate).FirstOrDefault();


                        if (lastbalance == null)
                        {
                            acc.LastBalance = 0;
                            acc.Balance = -tbl_payment_rcv.Amount;
                        }
                        else
                        {
                            acc.Balance = lastbalance.Balance - tbl_payment_rcv.Amount;
                            acc.LastBalance = lastbalance.Balance;
                        }

                        acc.IsDelete = "No";
                        acc.UserID = Convert.ToInt32(Session["UserID"]);
                        acc.CreateBy = Session["name"].ToString();
                        acc.CreateDate = DateTime.Now ;
                        acc.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                        //acc.UserID=null;
                        db.tbl_Account_Mst_Transaction.Add(acc);
                        db.SaveChanges();

                        if (invostatus == "Yes")
                        {
                            //PrintInvoice(tbl_stock.Stock_ID);
                            tbl_Payment_Rcv model = db.tbl_Payment_Rcv.Include(m => m.tbl_Employee).Where(m => m.Payment_ID == tbl_payment_rcv.Payment_ID).FirstOrDefault();
                            ////tbl_SaleMst mstRecrod = db.tbl_SaleMst.Find(saleMstId);
                            return View("Invoice", model);
                        }
                        tranScope.Complete();
                    }
                    catch (Exception ex) {
                        tranScope.Dispose();
                    }
                }

              


                return RedirectToAction("Index");
            }

            ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name", tbl_payment_rcv.EmpID);
            return View(tbl_payment_rcv);
        }


        public ActionResult PrintInvoice(int saleMstId)
        {
            //tbl_Payment_Rcv model = db.tbl_Payment_Rcv.Include(m => m.tbl_Employee).Where(m => m.Payment_ID == tbl_payment_rcv.Payment_ID).FirstOrDefault();
            tbl_Payment_Rcv model = db.tbl_Payment_Rcv.Find(saleMstId);
            return View("Invoice", model);


        }
        //
        // GET: /Payments/Edit/5

        public ActionResult Edit(int id = 0)
        {
            int ids = Convert.ToInt32(Session["Org_Code"]);
            tbl_Payment_Rcv tbl_payment_rcv = db.tbl_Payment_Rcv.Find(id);
            if (tbl_payment_rcv == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == ids), "EmpID", "Name", tbl_payment_rcv.EmpID);
            //ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name", tbl_payment_rcv.EmpID);
            return View(tbl_payment_rcv);
        }

        //
        // POST: /Payments/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Payment_Rcv tbl_payment_rcv)
        {
         //   ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name", tbl_payment_rcv.EmpID);
            if (ModelState.IsValid)
            {
                tbl_Account_Mst_Transaction VG = db.tbl_Account_Mst_Transaction.Where(a => a.Payment_ID == tbl_payment_rcv.Payment_ID).FirstOrDefault();
                VG.Particular = tbl_payment_rcv.Description + "(U)";
                VG.EmpID = tbl_payment_rcv.EmpID;
                VG.DR_Amount = tbl_payment_rcv.Amount;
                VG.CreateDate = tbl_payment_rcv.Recv_Date;
                VG.UpdateDate = DateTime.Now;
                VG.Payment_ID = tbl_payment_rcv.Payment_ID;
                VG.IsDelete = "No";
                VG.UserID = Convert.ToInt32(Session["UserID"]);
                
               // VG.CreateDate = DateTime.Now;
                VG.CreateBy = Session["name"].ToString();
                VG.Org_Id = Convert.ToInt32(Session["Org_Code"]);

                db.Entry(VG).State = EntityState.Modified;
                db.SaveChanges();


               // tbl_payment_rcv.p = Convert.ToInt32(Session["UserID"]);
                tbl_payment_rcv.Description = tbl_payment_rcv.Description + "-U";
                tbl_payment_rcv.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_payment_rcv.UpdateBy = Session["name"].ToString();
                tbl_payment_rcv.UpdateDate = DateTime.Now;
                tbl_payment_rcv.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_payment_rcv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name", tbl_payment_rcv.EmpID);
            return View(tbl_payment_rcv);
        }

        //
        // GET: /Payments/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Payment_Rcv tbl_payment_rcv = db.tbl_Payment_Rcv.Find(id);
            if (tbl_payment_rcv == null)
            {
                return HttpNotFound();
            }
            return View(tbl_payment_rcv);
        }

        //
        // POST: /Payments/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Account_Mst_Transaction VG = db.tbl_Account_Mst_Transaction.Where(a => a.Payment_ID == id).FirstOrDefault();
            db.tbl_Account_Mst_Transaction.Remove(VG);
            db.SaveChanges();
            tbl_Payment_Rcv tbl_payment_rcv = db.tbl_Payment_Rcv.Find(id);
            db.tbl_Payment_Rcv.Remove(tbl_payment_rcv);
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