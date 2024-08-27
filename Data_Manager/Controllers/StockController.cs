using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;
using Newtonsoft.Json;

namespace Data_Manager.Controllers
{
    public class StockController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /Stock/

        public ActionResult Index(int ids = 0)
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                if (ids > 0)
                {
                    return View(db.tbl_Stock.Where(a => a.Org_Id == id && a.StockMstId == ids).OrderByDescending(a => a.Stock_ID).Take(1000).ToList());
                }
                return View(db.tbl_Stock.OrderByDescending(a => a.Stock_ID).Take(1000).ToList());
            }

            if (ids > 0)
            {
                return View(db.tbl_Stock.Where(a => a.Org_Id == id && a.StockMstId == ids).OrderByDescending(a => a.Stock_ID).Take(1000).ToList());
            }
            return View(db.tbl_Stock.Where(a => a.Org_Id == id).OrderByDescending(a => a.Stock_ID).Take(1000).ToList());
           
        }


        public ActionResult StockReport()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();
            //ViewBag.HRCo_ = new SelectList(db.HRMS_Company_ST, "HRCo_", "Title");
           
            
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    cmd = new SqlCommand("[StockReport]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@org", SqlDbType.Int).Value = id;

                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt);
                    ViewBag.data = dt.Select().ToList();
                    //TempData["BasicSal"] = dt.Compute("Sum(N_Salary)", "").ToString();
                }
            
            return View();
        }

        //
        // GET: /Stock/Details/5

        public ActionResult Details(int id = 0 )
        {
            tbl_Stock tbl_stock = db.tbl_Stock.Find(id);
            if (tbl_stock == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stock);
        }

        //
        // GET: /Stock/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id), "SaleTypeID", "Name");
            //ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name");
            ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.Org_Id == id), "VendorId", "Name");
            return View();
        }

        public ActionResult Purchase_Return(tbl_Stock stock)
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id), "SaleTypeID", "Name");
            //ViewBag.VendorId = new SelectList(db.tbl_Vendor, "VendorId", "Name");
            ViewBag.VendorId = new SelectList(db.tbl_Vendor.Where(a => a.Org_Id == id), "VendorId", "Name");
            if (stock.VendorId > 0)
            {
                stock.Org_Id = id;
                stock.CreateDate = DateTime.Now;
                stock.CreateBy = Session["name"].ToString();
                stock.UserID = Convert.ToInt32(Session["UserID"]);
                db.tbl_Stock.Add(stock);
                db.SaveChanges();
            }

            
            return View();
        }

        //
        // POST: /Stock/Create

        public ActionResult PurchaseSave(string Details, string Master)
        {

            dynamic master = JsonConvert.DeserializeObject(Master);
            dynamic details = JsonConvert.DeserializeObject(Details);
            //POS_MasterSale s = entities.masterSalesLastInvoice();
            //int invoiceNo = 0;
            //if (s != null) // First Invoice is not new 
            //{
            //    invoiceNo = Convert.ToInt32(entities.masterSalesLastInvoice().InoviceNo);
            //}

            tbl_StockMst StockMst = new tbl_StockMst();
            foreach (var m in master)
            {
                StockMst.VendorId = m.VendorId;
                StockMst.ItemQuantity = m.ItemQuantity;
                StockMst.Discount = m.Discount;
                StockMst.TotalAmount = m.TotalAmount;
                StockMst.Date = m.Date;
                StockMst.UserID = Convert.ToInt32(Session["UserID"]);              
                StockMst.IsDelete = "N";
                StockMst.CreateDate = DateTime.Now;
                StockMst.CreateBy = Session["name"].ToString();
                StockMst.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_StockMst.Add(StockMst);
            }
            db.SaveChanges();
            int StockMstId = StockMst.StockMstId;


            foreach (var d in details)
            {
              
               string stid = d.SaleTypeID;               
               string[] SaleTypeID = stid.Split('-');                
                int sid = Convert.ToInt32(SaleTypeID[0]);
                tbl_Stock item = new tbl_Stock();
                
                    item.StockMstId = StockMstId;
                    item.PurchaseType = d.PurchaseType;
                    item.SaleTypeID = sid;                  
                    item.Quantity = d.Quantity;
                    item.Unit_Amount = d.Unit_Amount;
                    item.Sale_Amount = d.Sale_Amount;
                    item.Total_Amount = d.Total_Amount;
                    item.Description = d.Description;                  
                    item.IsDelete = "N";
                    item.CreateDate = DateTime.Now;
                    item.UserID = Convert.ToInt32(Session["UserID"]);
                    item.CreateBy = Session["name"].ToString();
                    item.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                
                db.tbl_Stock.Add(item);
                
            }
            db.SaveChanges();
            
            tbl_Vendors_Mst_Transaction acc = new tbl_Vendors_Mst_Transaction();
            foreach (var m in master)
            {
                acc.StockMstId = StockMstId;
                acc.Particular = "Purchased Item";
                acc.VendorId = m.VendorId;
                acc.DR_Amount = m.TotalAmount;

                acc.ActiveBalance = true;
                var lastbalance = db.tbl_Vendors_Mst_Transaction.Where(a => a.VendorId == acc.VendorId && a.ActiveBalance == true).OrderByDescending(a => a.CreateDate).FirstOrDefault();

                
                if (lastbalance == null)
                {
                    acc.LastBalance = 0;
                    acc.Balance = m.TotalAmount;
                }
                else
                {
                    acc.Balance = m.TotalAmount;
                    acc.Balance = lastbalance.Balance + acc.Balance;
                    acc.LastBalance = lastbalance.Balance;
                }

                acc.IsDelete = "No";
                acc.CreateDate = DateTime.Now;
                acc.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                acc.CreateBy = Session["name"].ToString();
                //acc.UserID=null;
                db.tbl_Vendors_Mst_Transaction.Add(acc);
                db.SaveChanges(); 
            }
            return RedirectToAction("Index");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Stock tbl_stock ,string invostatus)
        {
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.IsDelete == "N"), "SaleTypeID", "Name", tbl_stock.SaleTypeID);
            if (ModelState.IsValid)
            {

                tbl_stock.IsDelete = "No";
                tbl_stock.CreateDate = DateTime.Now;
                tbl_stock.CreateBy = "Admin";
                tbl_stock.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Stock.Add(tbl_stock);

                db.SaveChanges();

                tbl_Vendors_Mst_Transaction acc = new tbl_Vendors_Mst_Transaction();
                acc.Particular = tbl_stock.Description;
                //acc.Stock_ID = tbl_stock.Stock_ID;
                acc.VendorId = tbl_stock.VendorId;
                acc.DR_Amount = tbl_stock.Total_Amount;
                acc.IsDelete = "No";
                acc.CreateDate = DateTime.Now;
                acc.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                acc.CreateBy = "Admin";
                //acc.UserID=null;
                db.tbl_Vendors_Mst_Transaction.Add(acc);
                db.SaveChanges();
                //invostatus = "Yes";
                if (invostatus == "Yes")
                {
                    //PrintInvoice(tbl_stock.Stock_ID);
                    tbl_Stock model = db.tbl_Stock.Include(m => m.tbl_Vendor).Include(m=>m.tbl_SalesTypeSetupForm).Where(m=>m.Stock_ID == tbl_stock.Stock_ID).FirstOrDefault();
                    ////tbl_SaleMst mstRecrod = db.tbl_SaleMst.Find(saleMstId);
                    return View("Invoice", model);
                }


                return RedirectToAction("Index");
            }

            return View(tbl_stock);
        }

        public ActionResult PrintInvoice(int MstId)
        {
            tbl_StockMst model = db.tbl_StockMst.Find(MstId);
            
            //tbl_SaleMst mstRecrod = db.tbl_SaleMst.Find(saleMstId);
            return View("Invoice", model);


        }

        //
        // GET: /Stock/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_Stock tbl_stock = db.tbl_Stock.Find(id);
            ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.IsDelete == "N"), "SaleTypeID", "Name", tbl_stock.SaleTypeID);
            if (tbl_stock == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stock);
        }

        //
        // POST: /Stock/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Stock tbl_stock)
        {
            if (ModelState.IsValid)
            {
                ViewBag.SaleTypeID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.IsDelete == "N"), "SaleTypeID", "Name", tbl_stock.SaleTypeID);
               tbl_stock.Org_Id= Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_stock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_stock);
        }

        //
        // GET: /Stock/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_Stock tbl_stock = db.tbl_Stock.Find(id);
            if (tbl_stock == null)
            {
                return HttpNotFound();
            }
            return View(tbl_stock);
        }

        //
        // POST: /Stock/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Stock tbl_stock = db.tbl_Stock.Find(id);
            db.tbl_Stock.Remove(tbl_stock);
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