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
    public class MasterExpenceController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /MasterExpence/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_MstExpense.OrderByDescending(a=>a.ExpMstID).Take(1000).ToList());
            }
            return View(db.tbl_MstExpense.Where(a => a.Org_Id == id).OrderByDescending(a => a.ExpMstID).Take(1000).ToList());
        }

        //
        // GET: /MasterExpence/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_MstExpense tbl_mstexpense = db.tbl_MstExpense.Find(id);
            if (tbl_mstexpense == null)
            {
                return HttpNotFound();
            }
            return View(tbl_mstexpense);
        }

        //
        // GET: /MasterExpence/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MasterExpence/Create

        [HttpPost]
        public ActionResult Create(tbl_MstExpense tbl_mstexpense)
        {
            if (ModelState.IsValid)
            {
                tbl_mstexpense.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_mstexpense.UserID = Convert.ToInt32(Session["UserID"]);               
                tbl_mstexpense.IsDelete = "N";
                tbl_mstexpense.CreateDate = DateTime.Now;
                tbl_mstexpense.CreateBy = Session["name"].ToString();
                db.tbl_MstExpense.Add(tbl_mstexpense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_mstexpense);
        }

        //
        // GET: /MasterExpence/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_MstExpense tbl_mstexpense = db.tbl_MstExpense.Find(id);
            if (tbl_mstexpense == null)
            {
                return HttpNotFound();
            }
            return View(tbl_mstexpense);
        }

        //
        // POST: /MasterExpence/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_MstExpense tbl_mstexpense)
        {
            if (ModelState.IsValid)
            {
                tbl_mstexpense.Org_Id = Convert.ToInt32(Session["Org_Code"]);          
                tbl_mstexpense.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_mstexpense.IsDelete = "N";
                tbl_mstexpense.UpdateDate = DateTime.Now;
                tbl_mstexpense.UpdateBy = Session["name"].ToString();
                db.Entry(tbl_mstexpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_mstexpense);
        }

        //
        // GET: /MasterExpence/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_MstExpense tbl_mstexpense = db.tbl_MstExpense.Find(id);
            if (tbl_mstexpense == null)
            {
                return HttpNotFound();
            }
            return View(tbl_mstexpense);
        }

        //
        // POST: /MasterExpence/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_MstExpense tbl_mstexpense = db.tbl_MstExpense.Find(id);
            db.tbl_MstExpense.Remove(tbl_mstexpense);
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