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
    public class Vendor_PaymentController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Vendor_Payment/

        public ActionResult Index(int vendorid=0)
        {
            
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.IsDelete == "N" && a.Org_Id == id), "VendorId", "Name");
            if (id == 1)
            {
                return View(db.tbl_Vendor_Payment.Include(t => t.tbl_Vendor).OrderByDescending(a => a.Ven_Payment_ID).Take(1).ToList());
            }
            if (vendorid > 0)
            {
                var tbl_vendor_payment1 = db.tbl_Vendor_Payment.Include(t => t.tbl_Vendor).Where(a => a.Org_Id == id && a.VendorId==vendorid);
                return View(tbl_vendor_payment1.OrderByDescending(a => a.Ven_Payment_ID).Take(100).ToList());
            }
            var tbl_vendor_payment = db.tbl_Vendor_Payment.Include(t => t.tbl_Vendor).Where(a=>a.Org_Id == id);
            return View(tbl_vendor_payment.OrderByDescending(a =>a.Ven_Payment_ID).Take(100).ToList());
        }

        //
        // GET: /Vendor_Payment/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Vendor_Payment tbl_vendor_payment = db.tbl_Vendor_Payment.Find(id);
            if (tbl_vendor_payment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor_payment);
        }

        //
        // GET: /Vendor_Payment/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.IsDelete == "N" && a.Org_Id==id), "VendorId", "Name");
            //ViewBag.BankId = new SelectList(db.tbl_Bank.Where(a => a.isDelete == "N"), "BankId", "Name");
            //ViewBag.BankAccountId = new SelectList(db.tbl_Account.Where(a => a.isDelete == "N"), "BankAccountId", "AccountNo");
            ViewBag.BankAccountId = new SelectList(from ba in db.tbl_Account
                                              join b in db.tbl_Bank on ba.BankId equals b.BankId
                                              where ba.isDelete ==null && ba.Org_Id==id
                                              select new
                                              {
                                                  BankAccountId = ba.BankAccountId,
                                                  name = ba.AccountNo +"(" + b.Name + ")"
                                              }, "BankAccountId", "name");
            return View();
        }

        //
        // POST: /Vendor_Payment/Create

        [HttpPost]
       
        public ActionResult Create(tbl_Vendor_Payment tbl_vendor_payment, string invostatus)
        {
            //if (ModelState.IsValid)
            {
                using (TransactionScope tranScope = new TransactionScope())
                using (Entities_Data db = new Entities_Data())
                {
                    try
                    {
                        tbl_vendor_payment.UserID = Convert.ToInt32(Session["UserID"]);
                        tbl_vendor_payment.IsDelete = "N";
                        tbl_vendor_payment.CreateDate = DateTime.Now;
                        tbl_vendor_payment.CreateBy = Session["name"].ToString();
                        tbl_vendor_payment.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                        db.tbl_Vendor_Payment.Add(tbl_vendor_payment);
                        db.SaveChanges();


                        tbl_Vendors_Mst_Transaction acc = new tbl_Vendors_Mst_Transaction();
                        acc.Particular = tbl_vendor_payment.Description;
                        //acc.Stock_ID = tbl_stock.Stock_ID;
                        acc.VendorId = tbl_vendor_payment.VendorId;
                        acc.CR_Amount = tbl_vendor_payment.Amount;
                        acc.Ven_Payment_ID = tbl_vendor_payment.Ven_Payment_ID;
                        acc.ActiveBalance = true;
                        var lastbalance = db.tbl_Vendors_Mst_Transaction.Where(a => a.VendorId == acc.VendorId && a.ActiveBalance == true).OrderByDescending(a => a.CreateDate).FirstOrDefault();


                        if (lastbalance == null)
                        {
                            acc.LastBalance = 0;
                            acc.Balance = -tbl_vendor_payment.Amount;
                        }
                        else
                        {
                            acc.Balance = lastbalance.Balance - tbl_vendor_payment.Amount;
                            acc.LastBalance = lastbalance.Balance;
                        }


                        acc.IsDelete = "No";
                        acc.UserID = Convert.ToInt32(Session["UserID"]);
                        acc.CreateDate = DateTime.Now;
                        acc.CreateBy = Session["name"].ToString();
                        acc.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                        //acc.UserID=null;
                        db.tbl_Vendors_Mst_Transaction.Add(acc);
                        db.SaveChanges();
                        tranScope.Complete();
                        if (invostatus == "Yes")
                        {
                            //PrintInvoice(tbl_stock.Stock_ID);
                            tbl_Vendor_Payment model = db.tbl_Vendor_Payment.Include(m => m.tbl_Vendor).Where(m => m.Ven_Payment_ID == tbl_vendor_payment.Ven_Payment_ID).FirstOrDefault();
                            ////tbl_SaleMst mstRecrod = db.tbl_SaleMst.Find(saleMstId);
                            return View("Invoice", model);
                        }
                        return RedirectToAction("Index");
                        
                    }
                    catch (Exception ex)
                    {
                        tranScope.Dispose();
                    }
                }
                    
            }

            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_vendor_payment.VendorId);
            return View(tbl_vendor_payment);
        }

        public ActionResult PrintInvoice(int saleMstId)
        {
            //tbl_Payment_Rcv model = db.tbl_Payment_Rcv.Include(m => m.tbl_Employee).Where(m => m.Payment_ID == tbl_payment_rcv.Payment_ID).FirstOrDefault();
            tbl_Vendor_Payment model = db.tbl_Vendor_Payment.Find(saleMstId);
            return View("Invoice", model);


        }
        //
        // GET: /Vendor_Payment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Vendor_Payment tbl_vendor_payment = db.tbl_Vendor_Payment.Find(id);
            if (tbl_vendor_payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_vendor_payment.VendorId);
            ViewBag.BankAccountId = new SelectList(db.tbl_Account.Where(a => a.isDelete == "N"), "BankAccountId", "AccountNo", tbl_vendor_payment.BankAccountId);
            return View(tbl_vendor_payment);
        }

        //
        // POST: /Vendor_Payment/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Vendor_Payment tbl_vendor_payment)
        {
          //  tbl_Vendors_Mst_Transaction VG = new tbl_Vendors_Mst_Transaction();
            if (ModelState.IsValid)
            {

                tbl_Vendors_Mst_Transaction VG=db.tbl_Vendors_Mst_Transaction.Where(a=>a.Ven_Payment_ID == tbl_vendor_payment.Ven_Payment_ID).FirstOrDefault();
                VG.Particular = tbl_vendor_payment.Description + "(U)";
                VG.VendorId = tbl_vendor_payment.VendorId;
                VG.CR_Amount = tbl_vendor_payment.Amount;
               // VG.CreateDate=tbl_vendor_payment.CreateDate
                VG.UpdateDate = tbl_vendor_payment.UpdateDate;
                VG.Ven_Payment_ID = tbl_vendor_payment.Ven_Payment_ID;
                VG.IsDelete = "No";
                VG.UserID = Convert.ToInt32(Session["UserID"]);
                VG.CreateDate = DateTime.Now;
                VG.CreateBy = Session["name"].ToString();
                VG.Org_Id = Convert.ToInt32(Session["Org_Code"]);

                db.Entry(VG).State = EntityState.Modified;
                db.SaveChanges();
                tbl_vendor_payment.Description = tbl_vendor_payment.Description + "-U";  
                tbl_vendor_payment.UserID = Convert.ToInt32(Session["UserID"]);                
                tbl_vendor_payment.IsDelete = "N";
                tbl_vendor_payment.CreateDate = DateTime.Now;
                tbl_vendor_payment.CreateBy = Session["name"].ToString();
                tbl_vendor_payment.UpdateDate = DateTime.Now;
                tbl_vendor_payment.UpdateBy = Session["name"].ToString();
                tbl_vendor_payment.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_vendor_payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_vendor_payment.VendorId);
            return View(tbl_vendor_payment);
        }

        //
        // GET: /Vendor_Payment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Vendor_Payment tbl_vendor_payment = db.tbl_Vendor_Payment.Find(id);
            if (tbl_vendor_payment == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor_payment);
        }

        //
        // POST: /Vendor_Payment/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Vendors_Mst_Transaction VG = db.tbl_Vendors_Mst_Transaction.Where(a => a.Ven_Payment_ID == id).FirstOrDefault();
            db.tbl_Vendors_Mst_Transaction.Remove(VG);
            db.SaveChanges();
            tbl_Vendor_Payment tbl_vendor_payment = db.tbl_Vendor_Payment.Find(id);
            db.tbl_Vendor_Payment.Remove(tbl_vendor_payment);
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