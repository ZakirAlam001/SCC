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
    public class POSItemInController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /POSItemIn/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.POS_ItemIn.ToList());
            }

            
            return View(db.POS_ItemIn.Where(a => a.Org_Id == id).ToList());
        }

        //
        // GET: /POSItemIn/Details/5

        public ActionResult Details(int id = 0)
        {
            POS_ItemIn pos_itemin = db.POS_ItemIn.Find(id);
            if (pos_itemin == null)
            {
                return HttpNotFound();
            }
            return View(pos_itemin);
        }

        //
        // GET: /POSItemIn/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /POSItemIn/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(POS_ItemIn pos_itemin)
        {
            if (ModelState.IsValid)
            {
                pos_itemin.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.POS_ItemIn.Add(pos_itemin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pos_itemin);
        }

        //
        // GET: /POSItemIn/Edit/5

        public ActionResult Edit(int id = 0)
        {
            POS_ItemIn pos_itemin = db.POS_ItemIn.Find(id);
            if (pos_itemin == null)
            {
                return HttpNotFound();
            }
            return View(pos_itemin);
        }

        //
        // POST: /POSItemIn/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(POS_ItemIn pos_itemin)
        {
            if (ModelState.IsValid)
            {
                pos_itemin.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(pos_itemin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pos_itemin);
        }

        //
        // GET: /POSItemIn/Delete/5

        public ActionResult Delete(int id = 0)
        {
            POS_ItemIn pos_itemin = db.POS_ItemIn.Find(id);
            if (pos_itemin == null)
            {
                return HttpNotFound();
            }
            return View(pos_itemin);
        }

        //
        // POST: /POSItemIn/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POS_ItemIn pos_itemin = db.POS_ItemIn.Find(id);
            db.POS_ItemIn.Remove(pos_itemin);
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