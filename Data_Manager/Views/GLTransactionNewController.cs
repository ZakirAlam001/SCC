using Data_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Data_Manager.Views
{
    public class GLTransactionNewController : Controller
    {
        private Entities_Data db = new Entities_Data();
        //
        // GET: /GLTransactionNew/
        public class PartyHeaderVM
        {
            public int EmpID { get; set; }
            public string Name { get; set; }
            public string CellNo { get; set; }
            public decimal Balance { get; set; }
        }

        public class VendoHeaderVM
        {
            public int VendorId { get; set; }
            public string Name { get; set; }
            public string Cell { get; set; }
            public decimal Balance { get; set; }
        }
        public class PartyLedgerVM
        {
            public long Acc_Trans_ID { get; set; }

            public DateTime? CreateDate { get; set; }

            public string Name { get; set; }

            public string Particular { get; set; }

            public int? SaleMst_ID { get; set; }

            public int? Payment_ID { get; set; }

            public int? EmpID { get; set; }

            public decimal? DR_Amount { get; set; }

            public decimal? CR_Amount { get; set; }

            public decimal? Balance { get; set; }

            public string CreateBy { get; set; }
        }

        public class VendorLedgerVM
        {
            public long Vendor_Acc_Trans_ID { get; set; }

            public DateTime? CreateDate { get; set; }

            public string Name { get; set; }

            public string Particular { get; set; }

            public int? StockMstId { get; set; }

            public int? Ven_Payment_ID { get; set; }

            public int? VendorId { get; set; }

            public decimal? DR_Amount { get; set; }

            public decimal? CR_Amount { get; set; }

            public decimal? Balance { get; set; }

            public string CreateBy { get; set; }
        }
        public class PartySummaryVM
        {
            public decimal TotalSale { get; set; }

            public decimal TotalReceive { get; set; }
        }
        public ActionResult Sale()
        {
            return View();
        }
        public ActionResult Purchase()
        {
            return View();
        }
        public JsonResult GetPartyHeader(int empId)
        {
            var data = db.Database.SqlQuery<PartyHeaderVM>("EXEC sp_GetPartyHeader @EmpID", new SqlParameter("@EmpID", empId)).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurcahseHeader(int vendorid)
        {
            var data = db.Database.SqlQuery<VendoHeaderVM>("EXEC sp_GetPurchaseHeader @vendorID", new SqlParameter("@vendorID", vendorid)).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLedger(int empId, DateTime? fromDate, DateTime? toDate, string search)
        {
            var data = db.Database.SqlQuery<PartyLedgerVM>(
                "EXEC sp_GetPartyLedger @EmpID,@FromDate,@ToDate,@Search",

                new SqlParameter("@EmpID", empId),

                new SqlParameter("@FromDate", (object)fromDate ?? DBNull.Value),

                new SqlParameter("@ToDate", (object)toDate ?? DBNull.Value),

                new SqlParameter("@Search", (object)search ?? DBNull.Value)

            ).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPurchaseLedger(int vendorid, DateTime? fromDate, DateTime? toDate, string search)
        {
            var data = db.Database.SqlQuery<VendorLedgerVM>(
                "EXEC sp_GetVendorLedger @VendorID,@FromDate,@ToDate,@Search",

                new SqlParameter("@VendorID", vendorid),

                new SqlParameter("@FromDate", (object)fromDate ?? DBNull.Value),

                new SqlParameter("@ToDate", (object)toDate ?? DBNull.Value),

                new SqlParameter("@Search", (object)search ?? DBNull.Value)

            ).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetPartySummary(int empId)
        {
            var data = db.Database.SqlQuery<PartySummaryVM>(
                "EXEC sp_GetPartySummary @EmpID",
                new SqlParameter("@EmpID", empId)
            ).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVendorSummary(int vendorid)
        {
            var data = db.Database.SqlQuery<PartySummaryVM>(
                "EXEC sp_GetVendorSummary @VendorID",
                new SqlParameter("@VendorID", vendorid)
            ).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /GLTransactionNew/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /GLTransactionNew/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GLTransactionNew/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GLTransactionNew/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /GLTransactionNew/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GLTransactionNew/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /GLTransactionNew/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
