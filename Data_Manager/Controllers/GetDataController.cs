using Data_Manager.Models;
using Data_Manager.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Data_Manager.Controllers
{
   
    public class GetDataController : Controller
    {
        //
        // GET: /GetData/
        private Entities_Data db = new Entities_Data();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDataList(int Case, string date1)
        {

            DateTime date, to;
            date = to = DateTime.Now;

            if (date1 != "" && date1 != null)
            {
                date = Convert.ToDateTime(date1);
                date = date.Date;
            }
            else
            {
                date = DateTime.Now.Date;
            }

            var CaseID = new SqlParameter("@CaseID", Case);
            var Date = new SqlParameter("@date", date);

            if (Case == 1)
            {
                //Deposit
                var DepositData = db.tbl_deposit_Amount.Where(a=> EntityFunctions.TruncateTime(a.Date) == date).ToList();
                return Json(DepositData, JsonRequestBehavior.AllowGet);
            }
            else if (Case == 2)
            {
                //Sale
                var result = db.Database.SqlQuery<Resultset>("dbo.GetResults_ForDailySheets @CaseID,@date", CaseID,Date).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (Case == 3)
            {
                //Purchase
                var result = db.Database.SqlQuery<Resultset>("dbo.GetResults_ForDailySheets @CaseID,@date", CaseID, Date).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (Case == 4)
            { //Expense
                var result = db.Database.SqlQuery<Resultset>("dbo.GetResults_ForDailySheets @CaseID,@date", CaseID, Date).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (Case == 5)
            {
                //PaymentRecv
                var result = db.Database.SqlQuery<Resultset>("dbo.GetResults_ForDailySheets @CaseID,@date", CaseID, Date).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (Case == 6)
            {//PaymentSend
                var result = db.Database.SqlQuery<Resultset>("dbo.GetResults_ForDailySheets @CaseID,@date", CaseID, Date).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else if (Case == 7)
            {//Loan Amount
                var result = db.Database.SqlQuery<Resultset>("dbo.GetResults_ForDailySheets @CaseID,@date", CaseID, Date).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return null;

        }

    }
}
