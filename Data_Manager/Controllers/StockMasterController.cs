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
    public class StockMasterController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /StockMaster/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_StockMst.OrderByDescending(a=>a.StockMstId).Take(1000).ToList());
            }

            var tbl_stockmst = db.tbl_StockMst.Include(t => t.tbl_Vendor).Where(a => a.Org_Id == id).OrderByDescending(a => a.StockMstId).Take(1000);
            return View(tbl_stockmst.ToList());
        }

        //
        // GET: /StockMaster/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_StockMst tbl_stockmst = db.tbl_StockMst.Find(id);
            if (tbl_stockmst == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stockmst);
        }

        //
        // GET: /StockMaster/Create

        public ActionResult Create()
        {
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name");
            return View();
        }

        //
        // POST: /StockMaster/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_StockMst tbl_stockmst)
        {
            if (ModelState.IsValid)
            {
                db.tbl_StockMst.Add(tbl_stockmst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_stockmst.VendorId);
            return View(tbl_stockmst);
        }

        //
        // GET: /StockMaster/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_StockMst tbl_stockmst = db.tbl_StockMst.Find(id);
            if (tbl_stockmst == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_stockmst.VendorId);
            return View(tbl_stockmst);
        }

        //
        // POST: /StockMaster/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_StockMst tbl_stockmst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_stockmst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name", tbl_stockmst.VendorId);
            return View(tbl_stockmst);
        }

        //
        // GET: /StockMaster/Delete/5

        public ActionResult Delete(int id = 0)
        {

            string msg;
            //Accounts delete

            var data = db.tbl_Stock.Where(a => a.StockMstId == id).ToList() ?? null;
            int c = 0;
            foreach (var i in data)
            {
                tbl_Stock tbl_Stock = db.tbl_Stock.Find(i.Stock_ID);
                db.tbl_Stock.Remove(tbl_Stock);
                db.SaveChanges();
                c++;
            }

            tbl_Vendors_Mst_Transaction tbl_Vendors_Mst_Transaction = db.tbl_Vendors_Mst_Transaction.Where(a => a.StockMstId == id).FirstOrDefault();
            db.tbl_Vendors_Mst_Transaction.Remove(tbl_Vendors_Mst_Transaction);
            db.SaveChanges();
            msg = "GL One Record Delete ";
            //Sale Details Delete

           
            msg = (msg + "|" + c.ToString() + " Records sale Details Deleted ").ToString();
            //Master Sale Delete      
            tbl_StockMst tbl_StockMst = db.tbl_StockMst.Find(id);
            db.tbl_StockMst.Remove(tbl_StockMst);
            db.SaveChanges();
            msg = msg + "|" + "(1)Record Invoice Deleted";
            ViewBag.delmsg = msg;
            return RedirectToAction("Index", "StockMaster");
        }

        //
        // POST: /StockMaster/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_StockMst tbl_stockmst = db.tbl_StockMst.Find(id);
            db.tbl_StockMst.Remove(tbl_stockmst);
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