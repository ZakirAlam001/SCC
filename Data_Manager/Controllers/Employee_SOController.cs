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
    public class Employee_SOController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Employee_SO/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Employee.Include(t => t.tbl_Designation).ToList());
            }
            var tbl_employee = db.tbl_Employee.Include(t => t.tbl_Designation).Where(a => a.Org_Id == id);
            return View(tbl_employee.ToList());
        }

        //
        // GET: /Employee_SO/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Employee tbl_employee = db.tbl_Employee.Find(id);
            if (tbl_employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_employee);
        }

        //
        // GET: /Employee_SO/Create

        public ActionResult Create()
        {
            ViewBag.DesignID = new SelectList(db.tbl_Designation, "DesigID", "Name");
            return View();
        }

        //
        // POST: /Employee_SO/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Employee tbl_employee)
        {
            if (ModelState.IsValid)
            {
                tbl_employee.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                tbl_employee.UserID = Convert.ToInt32(Session["UserID"]);
              //  tbl_employee.IsActive = "Y";
                //tbl_employee.IsDelete = "N";
                tbl_employee.CreateDate = DateTime.Now;
                tbl_employee.CreateBy = Session["name"].ToString();
            
                db.tbl_Employee.Add(tbl_employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DesignID = new SelectList(db.tbl_Designation, "DesigID", "Name", tbl_employee.DesignID);
            return View(tbl_employee);
        }

        //
        // GET: /Employee_SO/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Employee tbl_employee = db.tbl_Employee.Find(id);
            if (tbl_employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignID = new SelectList(db.tbl_Designation, "DesigID", "Name", tbl_employee.DesignID);
            return View(tbl_employee);
        }

        //
        // POST: /Employee_SO/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Employee tbl_employee)
        {
            if (ModelState.IsValid)
            {
             
                tbl_employee.UserID = Convert.ToInt32(Session["UserID"]);
               // tbl_employee.IsActive = "Y";
                //tbl_employee.IsDelete = "N";
                tbl_employee.UpdateDate = DateTime.Now;
                tbl_employee.UpdateBy = Session["name"].ToString();
                tbl_employee.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignID = new SelectList(db.tbl_Designation, "DesigID", "Name", tbl_employee.DesignID);
            return View(tbl_employee);
        }

        //
        // GET: /Employee_SO/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Employee tbl_employee = db.tbl_Employee.Find(id);
            if (tbl_employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_employee);
        }

        //
        // POST: /Employee_SO/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Employee tbl_employee = db.tbl_Employee.Find(id);
            db.tbl_Employee.Remove(tbl_employee);
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