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
    public class SaleDetailsController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /SaleDetails/

        public ActionResult Index(int ids=0)
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                if (ids > 0)
                {
                    return View(db.tbl_SaleDetail.Include(t => t.tbl_SaleMaster).Include(t => t.tbl_SalesTypeSetupForm).Include(t => t.tbl_Stock).Where(a => a.IsDelete == "No" && a.ExpMstID == ids).OrderByDescending(a => a.SaleDetail_ID).Take(1000).ToList());
                }

                return View(db.tbl_SaleDetail.Include(t => t.tbl_SaleMaster).Include(t => t.tbl_SalesTypeSetupForm).Include(t => t.tbl_Stock).Where(a => a.IsDelete == "No").OrderByDescending(a => a.SaleDetail_ID).Take(1000).ToList());
            }
            if (ids > 0)
            {
                return View(db.tbl_SaleDetail.Include(t => t.tbl_SaleMaster).Include(t => t.tbl_SalesTypeSetupForm).Include(t => t.tbl_Stock).Where(a => a.IsDelete == "No" && a.ExpMstID == ids && a.Org_Id == id).OrderByDescending(a => a.SaleDetail_ID).Take(1000).ToList());
            }
            var tbl_saledetail = db.tbl_SaleDetail.Include(t => t.tbl_SaleMaster).Include(t => t.tbl_SalesTypeSetupForm).Include(t => t.tbl_Stock).Where(a => a.IsDelete == "No").Where(a => a.Org_Id == id).OrderByDescending(a => a.SaleDetail_ID).Take(1000);
            return View(tbl_saledetail.ToList());
        }
      
        // GET: /SaleDetails/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_SaleDetail tbl_saledetail = db.tbl_SaleDetail.Find(id);
            if (tbl_saledetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_saledetail);
        }

        //
        // GET: /SaleDetails/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.Sale_return_ID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name");
            ViewBag.ExpMstID = new SelectList(db.tbl_SaleMaster.OrderByDescending(e => e.SaleMst_ID).Where(a => a.Org_Id == id), "SaleMst_ID", "SaleMst_ID");
            //ViewBag.ExpMstID = new SelectList(db.tbl_SaleMaster, "SaleMst_ID", "Description");
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id), "SaleTypeID", "Name");
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock.Where(a => a.Org_Id == id), "Stock_ID", "Invoice_No");
            return View();
        }

        //
        // POST: /SaleDetails/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_SaleDetail tbl_saledetail)
        {
            if (ModelState.IsValid)
            {
                tbl_saledetail.Sale_Status = "R";
                tbl_saledetail.CreateDate = DateTime.Now;
                tbl_saledetail.IsDelete = "No";
                tbl_saledetail.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_SaleDetail.Add(tbl_saledetail);
                db.SaveChanges();

                ///GL Hit
                tbl_Account_Mst_Transaction acc = new tbl_Account_Mst_Transaction();
                acc.Particular ="(Sale Return* ) "+ tbl_saledetail.Description;
                //acc.SaleMst_ID = masterRecordId;

                acc.EmpID = tbl_saledetail.Sale_return_ID;
                acc.DR_Amount = tbl_saledetail.Net_Sale_Price;
                acc.IsDelete = "No";
                acc.CreateDate = DateTime.Now;
                acc.CreateBy = "Admin";
                acc.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                //acc.UserID=null;
                db.tbl_Account_Mst_Transaction.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpMstID = new SelectList(db.tbl_SaleMaster, "SaleMst_ID", "Description", tbl_saledetail.ExpMstID);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name", tbl_saledetail.SaleTypeID);
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No", tbl_saledetail.Stock_ID);
            return View(tbl_saledetail);
        }

        //
        // GET: /SaleDetails/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_SaleDetail tbl_saledetail = db.tbl_SaleDetail.Find(id);
            if (tbl_saledetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpMstID = new SelectList(db.tbl_SaleMaster, "SaleMst_ID", "Description", tbl_saledetail.ExpMstID);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name", tbl_saledetail.SaleTypeID);
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No", tbl_saledetail.Stock_ID);
            return View(tbl_saledetail);
        }

        //
        // POST: /SaleDetails/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_SaleDetail tbl_saledetail)
        {
            if (ModelState.IsValid)
            {
                tbl_saledetail.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_saledetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpMstID = new SelectList(db.tbl_SaleMaster, "SaleMst_ID", "Description", tbl_saledetail.ExpMstID);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm, "SaleTypeID", "Name", tbl_saledetail.SaleTypeID);
            ViewBag.Stock_ID = new SelectList(db.tbl_Stock, "Stock_ID", "Invoice_No", tbl_saledetail.Stock_ID);
            return View(tbl_saledetail);
        }

        //
        // GET: /SaleDetails/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_SaleDetail tbl_saledetail = db.tbl_SaleDetail.Find(id);
            if (tbl_saledetail == null)
            {
                return HttpNotFound();
            }
            return View(tbl_saledetail);
        }

        //
        // POST: /SaleDetails/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_SaleDetail tbl_saledetail = db.tbl_SaleDetail.Find(id);
            db.tbl_SaleDetail.Remove(tbl_saledetail);
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