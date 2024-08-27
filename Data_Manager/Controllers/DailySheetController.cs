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
    public class DailySheetController : Controller
    {
        //
        // GET: /DailySheet/
        private Entities_Data db = new Entities_Data();
        
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.Stock_ID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id), "SaleTypeID", "Name");
            ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name");
            ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.IsDelete == "N" && a.Org_Id == id), "VendorId", "Name");
            ViewBag.LoanPersonID = new SelectList(db.tbl_LoanPerson.Where(a => a.IsDelete == "N" && a.Org_Id == id), "LoanPersonID", "Name");
            return View();
        }

    }
}
