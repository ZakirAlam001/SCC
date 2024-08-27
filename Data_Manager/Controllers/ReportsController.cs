using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        private Entities_Data db = new Entities_Data();
        public ActionResult Sales()
        {

            return View();
        }
        public ActionResult Purchase()
        {

            return View();
        }
        public ActionResult SalesWithPayement()
        {

            return View();
        }
        public ActionResult PuchaseWithPayement()
        {

            return View();
        }
        public ActionResult Consolidate()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name");
            return View();
        }

        public ActionResult Daily_Report(string Date="")
        {
            DateTime dt=DateTime.Now;
            dt=dt.Date;
            //Deposit amount
            TempData["Deposit"] = Convert.ToInt32((db.tbl_deposit_Amount.Where(a => a.Date == dt && a.DepositType == "d").Select(a => a.Amount).Sum()) ?? 0);
            //Cash Collect from cash box
            TempData["Cash_collect"] = Convert.ToInt32((db.tbl_deposit_Amount.Where(a => a.Date == dt && a.DepositType == "c").Select(a => a.Amount).Sum()) ?? 0);
            //Cash left
            TempData["Cash_left"] = Convert.ToInt32((db.tbl_deposit_Amount.Where(a => a.Date == dt && a.DepositType == "cl").Select(a => a.Amount).Sum()) ?? 0);
            //Advance Payments
            TempData["advance"] = Convert.ToInt32((db.tbl_deposit_Amount.Where(a => a.Date == dt && a.DepositType == "ad").Select(a => a.Amount).Sum()) ?? 0);
            
            //daily expense sum
            TempData["Sum_Expense"] = Convert.ToInt32((db.tbl_MstExpense.Where(a => EntityFunctions.TruncateTime(a.ExpDate) == dt).Select(a => a.TotalAmount).Sum()) ?? 0);
            
            //Mall Bach gya ho
            TempData["Sum_StockRemaining"] = Convert.ToInt32((db.tbl_StockDaily.Where(a => a.Date == dt).Select(a => a.Amount).Sum()) ?? 0);
            //freezer se mall rakha gya pesa (in)
            TempData["Sum_StockRemaining_in"] = Convert.ToInt32((db.tbl_oldstockinout.Where(a => a.Date == dt && a.StockType == "i").Select(a => a.Amount).Sum()) ?? 0);
            //freezer me mall wapis rakhne wala (out)
            TempData["Sum_StockRemaining_out"] = Convert.ToInt32((db.tbl_oldstockinout.Where(a => a.Date == dt && a.StockType == "o").Select(a => a.Amount).Sum()) ?? 0);
            //Balance Record
            ViewBag.abcbalance = db.tbl_daily_balance.Where(a => a.CreateDate == dt && a.BalanceAmount > 0).ToList() ?? null;
            

                     
            ///sps Call
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                cmd = new SqlCommand("[DailySale_Purcase_report]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = "s";
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = dt;

                cmd.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt1);
                var sales = dt1.Select().ToList();


                ViewBag.Sales = sales;

                cmd = new SqlCommand("[DailySale_Purcase_report]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@type", SqlDbType.NVarChar).Value = "p";
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = dt;
                cmd.ExecuteNonQuery();

                DataTable dt2 = new DataTable();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd);

                adp1.Fill(dt2);
                var Purchase = dt2.Select().ToList();

                ViewBag.Purchase = Purchase;

            }
            
            
            return View();
        }

       

    }
}
