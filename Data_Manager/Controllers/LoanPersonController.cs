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
    public class LoanPersonController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /LoanPerson/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_LoanPerson.ToList());
            }
            return View(db.tbl_LoanPerson.Where(a=>a.Org_Id == id).ToList());
        }

        //
        // GET: /LoanPerson/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_LoanPerson tbl_loanperson = db.tbl_LoanPerson.Find(id);
            if (tbl_loanperson == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loanperson);
        }

        //
        // GET: /LoanPerson/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LoanPerson/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_LoanPerson tbl_loanperson)
        {
            if (ModelState.IsValid)
            {
                tbl_loanperson.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_loanperson.UserID = Convert.ToInt32(Session["UserID"]);
               
               // tbl_loanperson.IsDelete = "N";
                tbl_loanperson.CreateDate = DateTime.Now;
                tbl_loanperson.CreateBy = Session["name"].ToString();
                
                db.tbl_LoanPerson.Add(tbl_loanperson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_loanperson);
        }

        //
        // GET: /LoanPerson/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_LoanPerson tbl_loanperson = db.tbl_LoanPerson.Find(id);
            if (tbl_loanperson == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loanperson);
        }

        //
        // POST: /LoanPerson/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_LoanPerson tbl_loanperson)
        {
            if (ModelState.IsValid)
            {
                tbl_loanperson.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_loanperson.UserID = Convert.ToInt32(Session["UserID"]);

                // tbl_loanperson.IsDelete = "N";
                tbl_loanperson.UpdateDate = DateTime.Now;
                tbl_loanperson.UpdateBy = Session["name"].ToString();
                db.Entry(tbl_loanperson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_loanperson);
        }

        //
        // GET: /LoanPerson/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_LoanPerson tbl_loanperson = db.tbl_LoanPerson.Find(id);
            if (tbl_loanperson == null)
            {
                return HttpNotFound();
            }
            return View(tbl_loanperson);
        }

        //
        // POST: /LoanPerson/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_LoanPerson tbl_loanperson = db.tbl_LoanPerson.Find(id);
            db.tbl_LoanPerson.Remove(tbl_loanperson);
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