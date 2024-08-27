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
    public class Vendor_TransactionsController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Vendor_Transactions/

        public ActionResult Index(int VendorId = 0)
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.IsDelete == "N"), "VendorId", "Name");
            }
            else
            {
                ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.IsDelete == "N" && a.Org_Id==id), "VendorId", "Name");
            }
          
            
            if (id == 1)
            {
                if (VendorId > 0)
                {
                    return View(db.tbl_Vendors_Mst_Transaction.Where(a => a.IsDelete == "No" && a.VendorId == VendorId).OrderByDescending(a => a.Vendor_Acc_Trans_ID).ToList());
                }
                return View(db.tbl_Vendors_Mst_Transaction.Where(a => a.IsDelete == "No").OrderByDescending(a => a.Vendor_Acc_Trans_ID).Take(10).ToList());
            }

            if (VendorId > 0)
            {
                return View(db.tbl_Vendors_Mst_Transaction.Where(a => a.IsDelete == "No" && a.VendorId == VendorId && a.Org_Id== id).OrderByDescending(a => a.Vendor_Acc_Trans_ID).ToList());
            }
            return View(db.tbl_Vendors_Mst_Transaction.Where(a => a.IsDelete == "No"  && a.Org_Id== id).OrderByDescending(a => a.Vendor_Acc_Trans_ID).Take(10).ToList());
        }

        //public ActionResult Index()
        //{
        //    ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.IsDelete == "N"), "VendorId", "Name");
        //    var tbl_vendors_mst_transaction = db.tbl_Vendors_Mst_Transaction.Include(t => t.tbl_Vendor).Include(t => t.tbl_Stock);
        //    return View(tbl_vendors_mst_transaction.ToList());
        //}

        //
        // GET: /Vendor_Transactions/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Vendors_Mst_Transaction tbl_vendors_mst_transaction = db.tbl_Vendors_Mst_Transaction.Find(id);
            if (tbl_vendors_mst_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendors_mst_transaction);
        }

        //
        // GET: /Vendor_Transactions/Create

        public ActionResult Create()
        {
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name");
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No");
            return View();
        }

        //
        // POST: /Vendor_Transactions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Vendors_Mst_Transaction tbl_vendors_mst_transaction)
        {
            if (ModelState.IsValid)
            {
                tbl_vendors_mst_transaction.Org_Id = Convert.ToInt32(Session["Org_Code"]); 
                db.tbl_Vendors_Mst_Transaction.Add(tbl_vendors_mst_transaction);
                tbl_vendors_mst_transaction.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_vendors_mst_transaction.CreateBy = Session["name"].ToString();
                tbl_vendors_mst_transaction.CreateDate = DateTime.Now;
                tbl_vendors_mst_transaction.IsDelete = "No";
                tbl_vendors_mst_transaction.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_vendors_mst_transaction.VendorId);
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No");
            return View(tbl_vendors_mst_transaction);
        }

        //
        // GET: /Vendor_Transactions/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Vendors_Mst_Transaction tbl_vendors_mst_transaction = db.tbl_Vendors_Mst_Transaction.Find(id);
            if (tbl_vendors_mst_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_vendors_mst_transaction.VendorId);
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No");
            return View(tbl_vendors_mst_transaction);
        }

        //
        // POST: /Vendor_Transactions/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Vendors_Mst_Transaction tbl_vendors_mst_transaction)
        {
            if (ModelState.IsValid)
            {
                tbl_vendors_mst_transaction.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_vendors_mst_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_vendors_mst_transaction.VendorId);
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No");
            return View(tbl_vendors_mst_transaction);
        }

        //
        // GET: /Vendor_Transactions/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Vendors_Mst_Transaction tbl_vendors_mst_transaction = db.tbl_Vendors_Mst_Transaction.Find(id);
            if (tbl_vendors_mst_transaction == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendors_mst_transaction);
        }

        //
        // POST: /Vendor_Transactions/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Vendors_Mst_Transaction tbl_vendors_mst_transaction = db.tbl_Vendors_Mst_Transaction.Find(id);
            db.tbl_Vendors_Mst_Transaction.Remove(tbl_vendors_mst_transaction);
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