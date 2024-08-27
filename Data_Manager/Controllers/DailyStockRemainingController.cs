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
    public class DailyStockRemainingController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /DailyStockRemaining/

        public ActionResult Index()
        {
            return View(db.tbl_StockDaily.ToList());
        }

        //
        // GET: /DailyStockRemaining/Details/5

        public ActionResult Details(long id = 0)
        {
            tbl_StockDaily tbl_stockdaily = db.tbl_StockDaily.Find(id);
            if (tbl_stockdaily == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stockdaily);
        }

        //
        // GET: /DailyStockRemaining/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DailyStockRemaining/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_StockDaily tbl_stockdaily)
        {
            if (ModelState.IsValid)
            {
                db.tbl_StockDaily.Add(tbl_stockdaily);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_stockdaily);
        }

        //
        // GET: /DailyStockRemaining/Edit/5

        public ActionResult Edit(long id = 0)
        {
            tbl_StockDaily tbl_stockdaily = db.tbl_StockDaily.Find(id);
            if (tbl_stockdaily == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stockdaily);
        }

        //
        // POST: /DailyStockRemaining/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_StockDaily tbl_stockdaily)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_stockdaily).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_stockdaily);
        }

        //
        // GET: /DailyStockRemaining/Delete/5

        public ActionResult Delete(long id = 0)
        {
            tbl_StockDaily tbl_stockdaily = db.tbl_StockDaily.Find(id);
            if (tbl_stockdaily == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stockdaily);
        }

        //
        // POST: /DailyStockRemaining/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_StockDaily tbl_stockdaily = db.tbl_StockDaily.Find(id);
            db.tbl_StockDaily.Remove(tbl_stockdaily);
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