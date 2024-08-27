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
    public class CustomerController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Customer/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Customer.ToList());
            }

            return View(db.tbl_Customer.Where(a => a.Org_Id == id).ToList());


            
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            if (tbl_customer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()

        {   //ViewBag.DesignID = new SelectList(db.tbl_Designation, "DesigID", "Name");
            ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name");
            ViewBag.EmployeeID = new SelectList(db.tbl_Employee.Where(a => a.IsDelete == "N" && a.IsActive == "Y"), "EmpID", "Name");
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Customer tbl_customer)
        {
            if (ModelState.IsValid)
            {
                tbl_customer.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Customer.Add(tbl_customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.tbl_Employee.Where(a => a.IsDelete == "N" && a.IsActive == "Y"), "EmpID", "Name", tbl_customer.EmployeeID);
            return View(tbl_customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            ViewBag.EmployeeID = new SelectList(db.tbl_Employee.Where(a => a.IsDelete == "N" && a.IsActive == "Y"), "EmpID", "Name", tbl_customer.EmployeeID); ViewBag.EmployeeID = new SelectList(db.tbl_Employee.Where(a => a.IsDelete == "N" && a.IsActive == "Y"), "EmpID", "Name", tbl_customer.EmployeeID);
            if (tbl_customer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_customer);

        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Customer tbl_customer)
        {
            ViewBag.EmployeeID = new SelectList(db.tbl_Employee.Where(a => a.IsDelete == "N" && a.IsActive == "Y"), "EmpID", "Name", tbl_customer.EmployeeID);
            if (ModelState.IsValid)
            {
                tbl_customer.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            if (tbl_customer == null)
            {
                return HttpNotFound();
            }
            return View(tbl_customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Customer tbl_customer = db.tbl_Customer.Find(id);
            db.tbl_Customer.Remove(tbl_customer);
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