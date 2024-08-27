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
    public class Reports_ChartController : Controller
    {
        //
        // GET: /Reports_Chart/
        private Entities_Data db = new Entities_Data();
        public ActionResult Index( string date1=null)
        {
          
            try {
                int id = Convert.ToInt32(Session["Org_Code"]);

                DateTime date, to;
                date = to = DateTime.Now;

                if (date1 != "" && date1 !=null)
                {
                    date = Convert.ToDateTime(date1);
                    date = date.Date;
                }
                else
                {
                    date = DateTime.Now.Date;
                }

             //   date = date.Date ;

                TempData["date"] = date;

                //if (date1 != null & date1 != "")
                //{
                //     date = DateTime.Now.Date;
                //}
                //else
                //{
                //     date = DateTime.Now.Date;
                //}
                
                //Line Graph Sales
                var sale = db.tbl_SaleMaster.Where(a => a.Org_Id == id && EntityFunctions.TruncateTime(a.CreateDate) == date ).Sum(a => a.Total_Amount) ?? 0;
                ViewBag.Sale =sale;
                //Line Graph Purchase
                var Purchase = db.tbl_StockMst.Where(a => a.Org_Id == id && EntityFunctions.TruncateTime(a.CreateDate) == date).Sum(a => a.TotalAmount) ?? 0;
                ViewBag.Purchase = Purchase;
                //Line Graph Expense
                var Expense = db.tbl_MstExpense.Where(a => a.Org_Id == id && EntityFunctions.TruncateTime(a.CreateDate) == date).Sum(a => a.TotalAmount) ?? 0;
                ViewBag.Expense = Expense;

                //Line Graph Expense
                var ProductSaleCount = db.tbl_SaleDetail.Where(a => a.Org_Id == id && EntityFunctions.TruncateTime(a.CreateDate) == date).ToList() ?? null;
                int count=0;
                foreach (var i in ProductSaleCount)
                {
                    count += Convert.ToInt32(i.Stock_Counts);
                }
                ViewBag.ProductSaleCount = count;
                               //Line Graph CustomerBalance
                decimal Cr = db.tbl_Account_Mst_Transaction.Where(a => a.Org_Id == id ).Sum(a => a.CR_Amount) ?? 0;
                decimal Dr = db.tbl_Account_Mst_Transaction.Where(a => a.Org_Id == id ).Sum(a => a.DR_Amount) ?? 0;

                ViewBag.CCr = Cr;
                ViewBag.Cdr = Dr;
                //Line Graph VendorBalance
                decimal VCr = db.tbl_Vendors_Mst_Transaction.Where(a => a.Org_Id == id ).Sum(a => a.CR_Amount) ?? 0;
                decimal VDr = db.tbl_Vendors_Mst_Transaction.Where(a => a.Org_Id == id ).Sum(a => a.DR_Amount) ?? 0;

                ViewBag.VCr = VCr;
                ViewBag.VDr = VDr;

              //  <------------------------------->

                List<int> Month_Sale = new List<int>();
                List<int> Month_Purchase = new List<int>();
                List<int> Month_Expense = new List<int>();
                List<int> Month_Pnl = new List<int>();
                

               // int month = 1;
               
                for (int month = 0; month <= 12; month++)
                {
                    //Sale
                    var Sale = db.tbl_SaleMaster.Where(a => a.Org_Id == id && a.ApplyDate.Value.Month == month && a.ApplyDate.Value.Year == date.Year).Sum(a => a.Total_Amount) ?? 0;
                    Month_Sale.Add(Convert.ToInt32(Sale));
                    //Purchase

                    var Purchase_ = db.tbl_StockMst.Where(a => a.Org_Id == id && a.Date.Value.Month == month && a.Date.Value.Year == date.Year).Sum(a => a.TotalAmount) ?? 0;
                    Month_Purchase.Add(Convert.ToInt32(Purchase_));
                    //Expense

                    var Expense_ = db.tbl_MstExpense.Where(a => a.Org_Id == id && a.ExpDate.Value.Month == month && a.ExpDate.Value.Year == date.Year).Sum(a => a.TotalAmount) ?? 0;
                    Month_Expense.Add(Convert.ToInt32(Expense_));

                    //profit
                    var profit = db.tbl_SaleDetail.Where(a => a.Org_Id == id && a.CreateDate.Value.Month == month && a.CreateDate.Value.Year == date.Year).Sum(a => a.Profit_Amount) ?? 0;
                    Month_Pnl.Add(Convert.ToInt32(profit-Expense_));

                }



                int[] M_Sale_val     = Month_Sale.ToArray();
                int[] M_Purchase_val = Month_Purchase.ToArray();
                int[] M_Expenase_val = Month_Expense.ToArray();
                int[] M_Profit_val = Month_Pnl.ToArray();

                ViewBag.M_Sale_val = M_Sale_val;
                ViewBag.M_Purchase_val = M_Purchase_val;
                ViewBag.M_Expenase_val = M_Expenase_val;
                ViewBag.M_Profit_val = M_Profit_val;

                List<int> Daily_Sale = new List<int>();
                List<int> Daily_Purchase = new List<int>();
                List<int> Daily_Expense = new List<int>();
                List<int> Daily_Profit = new List<int>();
                for (int daily = 0; daily <= 31; daily++)
                {
                    //Sale
                    var Sale = db.tbl_SaleMaster.Where(a => a.Org_Id == id && a.ApplyDate.Value.Day == daily && a.ApplyDate.Value.Month == date.Month && a.ApplyDate.Value.Year == date.Year).Sum(a => a.Total_Amount) ?? 0;
                    Daily_Sale.Add(Convert.ToInt32(Sale));
                    //Purchase

                    var Purchase_ = db.tbl_StockMst.Where(a => a.Org_Id == id && a.Date.Value.Day == daily && a.Date.Value.Month == date.Month && a.Date.Value.Year == date.Year).Sum(a => a.TotalAmount) ?? 0;
                    Daily_Purchase.Add(Convert.ToInt32(Purchase_));
                    //Expense

                    var Expense_ = db.tbl_MstExpense.Where(a => a.Org_Id == id && a.ExpDate.Value.Day == daily && a.ExpDate.Value.Month == date.Month && a.ExpDate.Value.Year == date.Year).Sum(a => a.TotalAmount) ?? 0;
                    Daily_Expense.Add(Convert.ToInt32(Expense_));

                    //profit
                    var profit_ = db.tbl_SaleDetail.Where(a => a.Org_Id == id && a.CreateDate.Value.Day == daily && a.CreateDate.Value.Month == date.Month && a.CreateDate.Value.Year == date.Year).Sum(a => a.Profit_Amount) ?? 0;
                    Daily_Profit.Add(Convert.ToInt32(profit_ - Expense_));

                }

                int[] D_Sale_val = Daily_Sale.ToArray();
                int[] D_Purchase_val = Daily_Purchase.ToArray();
                int[] D_Expenase_val = Daily_Expense.ToArray();
                int[] D_Profit_val = Daily_Profit.ToArray();


                ViewBag.D_Sale_val = D_Sale_val;
                ViewBag.D_Purchase_val = D_Purchase_val;
                ViewBag.D_Expense_val = D_Expenase_val;
                ViewBag.D_Profit_val = D_Profit_val;

                TempData["Month"] = String.Format("{0:MMM/yy}", date.Date);
                TempData["year"] = String.Format("{0:yyyy}", date.Date);

            List<string> PiChart = new List<string>();
            

            ////Start for pie chart
            //if (id == 1)
            //{
            //    var vendor = db.tbl_Vendor.ToList();
            //    foreach (var item in vendor)
            //        {
            //            PiChart.Add("'" + item.Name + "',50");              
            //        }
            //}else{
            //    var vendor = db.tbl_Vendor.Where(a=>a.Org_Id == id) .ToList();
            //    foreach (var item in vendor)
            //        {
            //            PiChart.Add("'" + item.Name + "',50");              
            //        }
            //}
            //End for pie chart

            //Start for Single Bar chart

            //string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            //SqlConnection conn = new SqlConnection(con);
            //SqlCommand cmd = new SqlCommand();
            ////ViewBag.HRCo_ = new SelectList(db.HRMS_Company_ST, "HRCo_", "Title");


            //if (conn.State != ConnectionState.Open)
            //{
            //    conn.Open();
            //    cmd = new SqlCommand("[BarSalesGraph]", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    cmd.Parameters.Add("@year", SqlDbType.NVarChar).Value = "2018";
            //    cmd.Parameters.Add("@Org_Id", SqlDbType.NVarChar).Value = Session["Org_Code"].ToString();
               
            //    cmd.ExecuteNonQuery();
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

            //    adp.Fill(dt);
            //    var data = dt.Select().ToList();
            //    decimal jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            //   // if()
            //    jan = Convert.ToDecimal(data[0].ItemArray[3] ?? 0);
            //    feb = Convert.ToDecimal(data[1].ItemArray[3] ?? 0);
            //    mar = Convert.ToDecimal(data[2].ItemArray[3] ?? 0);
            //    apr = Convert.ToDecimal(data[3].ItemArray[3] ?? 0);
            //    may = Convert.ToDecimal(data[4].ItemArray[3] ?? 0);
            //    jun = Convert.ToDecimal(data[5].ItemArray[3] ?? 0);
            //    jul = Convert.ToDecimal(data[6].ItemArray[3] ?? 0 );
            //    aug = Convert.ToDecimal(data[7].ItemArray[3] ?? 0);
            //    sep = Convert.ToDecimal(data[8].ItemArray[3] ?? 0);
            //    oct = Convert.ToDecimal(data[9].ItemArray[3] ?? 0);
            //    nov = Convert.ToDecimal(data[10].ItemArray[3] ?? 0);
            //    dec = Convert.ToDecimal(data[11].ItemArray[3] ?? 0);
            // //   jan = Convert.ToDecimal(data[12]);
            //    //foreach( var i in data){


            //    //}
            //  //  ViewBag.data = dt.Select().ToList();
            //    //TempData["BasicSal"] = dt.Compute("Sum(N_Salary)", "").ToString();
            //    List<string> SBarChart = new List<string>();
            //    if (id == 1)
            //    {
            //        SBarChart.Add(""+jan+","+feb+","+ mar+","+apr+","+ may+","+jun+","+jul+","+aug+","+sep+","+oct+","+nov+","+dec+"");
            //    }
            //    else
            //    {

            //    }

            //    ViewBag.list = PiChart.ToArray();
            //    ViewBag.SBarChart = SBarChart.ToArray();
            //}


            
           
            //End  for Single Bar chart
           //New Graph Working Monthly Daily basis

//int[] terms = termsList.ToArray();
               //Start For Month
            

                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Consolidate_Report()
        {
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();
          
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                cmd = new SqlCommand("[Purhcase_and_Sale_Report]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@purchase", SqlDbType.NVarChar).Value = "purchase";
                cmd.Parameters.Add("@sale", SqlDbType.NVarChar).Value = "sale1";

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt);
                var data = dt.Select().ToList();
                

                ViewBag.Purchase = data;

                cmd = new SqlCommand("[Purhcase_and_Sale_Report]", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@purchase", SqlDbType.NVarChar).Value = "purchase1";
                cmd.Parameters.Add("@sale", SqlDbType.NVarChar).Value = "sale";
                cmd.ExecuteNonQuery();
                
                DataTable dt1 = new DataTable();
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd);

                adp1.Fill(dt1);
                var sale = dt1.Select().ToList();

                ViewBag.sale = sale;

            }
            return View();
        }
    }
}
