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
    public class POSMasterSaleController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /POSMasterSale/

        public ActionResult Index(int search = 1)
        {
            if (search < 1)
            {
                var baselineDate = DateTime.Now.AddDays(search);
                return View(db.POS_MasterSale.Where(a => EntityFunctions.TruncateTime(a.Detetime) >= EntityFunctions.TruncateTime(baselineDate)).OrderByDescending(a=>a.ID_MS).ToList());
            }          
            return View(db.POS_MasterSale.OrderByDescending(a=>a.ID_MS).Take(0).ToList());
        }

        //
        // GET: /POSMasterSale/Details/5

        public ActionResult Details(int id = 0)
        {
            POS_MasterSale pos_mastersale = db.POS_MasterSale.Find(id);
            if (pos_mastersale == null)
            {
                return HttpNotFound();
            }
            return View(pos_mastersale);
        }

        //
        // GET: /POSMasterSale/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /POSMasterSale/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(POS_MasterSale pos_mastersale)
        {
            if (ModelState.IsValid)
            {
                db.POS_MasterSale.Add(pos_mastersale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pos_mastersale);
        }

        //
        // GET: /POSMasterSale/Edit/5

        public ActionResult Edit(int id = 0)
        {
            POS_MasterSale pos_mastersale = db.POS_MasterSale.Find(id);
            if (pos_mastersale == null)
            {
                return HttpNotFound();
            }
            return View(pos_mastersale);
        }

        //
        // POST: /POSMasterSale/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(POS_MasterSale pos_mastersale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pos_mastersale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pos_mastersale);
        }

        //
        // GET: /POSMasterSale/Delete/5

        public ActionResult Delete(int id = 0)
        {
            POS_MasterSale pos_mastersale = db.POS_MasterSale.Find(id);
            if (pos_mastersale == null)
            {
                return HttpNotFound();
            }
            return View(pos_mastersale);
        }

        //
        // POST: /POSMasterSale/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POS_MasterSale pos_mastersale = db.POS_MasterSale.Find(id);
            db.POS_MasterSale.Remove(pos_mastersale);
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