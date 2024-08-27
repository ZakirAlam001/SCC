using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;
using Newtonsoft.Json;

namespace Data_Manager.Controllers
{
    public class SaleMasterController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /SaleMaster/

        //POSData for reading json bulk record
        public ActionResult Summery(string fromdate, string todate)
        {
            //string fromdate = "26-Sep-2017";
            //string todate = "26-Sep-2017";
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();
            //ViewBag.HRCo_ = new SelectList(db.HRMS_Company_ST, "HRCo_", "Title");

            DateTime from, to;
            from = to = DateTime.Now;

            if (fromdate != "")
                from = Convert.ToDateTime(fromdate);
            if (todate != "")
                to = Convert.ToDateTime(todate);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                cmd = new SqlCommand("[DailySummery]", conn);
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@fromdate", SqlDbType.Date).Value = from.Date;
                cmd.Parameters.Add("@todate", SqlDbType.Date).Value = to.Date;
               
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt);
                ViewBag.Summery = dt.Select().ToList();

                TempData["Date"] = (fromdate + "-to-" + todate).ToString();
                ViewBag.printtext = (fromdate + "-to-" + todate).ToString();
                TempData["TotalMilk"] = db.tbl_Stock.Where(a => a.tbl_SalesTypeSetupForm.Name == "Fresh Milk").Sum(a => a.Quantity);
                TempData["SaleMilk"] = db.POS_ItemOut.Where(a => a.POS_ItemIn.Name == "Fresh Milk").Sum(a => a.Quantity);

                TempData["Milkrate"] = db.POS_ItemIn.Where(a => a.Name == "Fresh Milk").OrderByDescending(a=>a.ID_ItemIn).FirstOrDefault().RetailPrice;
                ViewBag.Purchase = db.tbl_Stock.Where(a => EntityFunctions.TruncateTime(a.CreateDate) >= from.Date && EntityFunctions.TruncateTime(a.CreateDate) <= to.Date).ToList();
                ViewBag.VenPay = db.tbl_Vendor_Payment.Where(a => EntityFunctions.TruncateTime(a.Recv_Date) >= from.Date && EntityFunctions.TruncateTime(a.Recv_Date) <= to.Date).ToList(); ;
            }
            return View();
        }




        public int SaleSave(string Details , string Master)
        {




            dynamic master = JsonConvert.DeserializeObject(Master);
            dynamic details = JsonConvert.DeserializeObject(Details);
            //POS_MasterSale s = entities.masterSalesLastInvoice();
            //int invoiceNo = 0;
            //if (s != null) // First Invoice is not new 
            //{
            //    invoiceNo = Convert.ToInt32(entities.masterSalesLastInvoice().InoviceNo);
            //}

            POS_MasterSale sales = new POS_MasterSale();
            foreach (var m in master)
            {
                sales.Detetime = DateTime.Now;
                sales.UserID = 1;
                sales.No_of_Item = Details.Length;
                sales.Net_Amount = m.NetAmount;
                sales.Cash_Rcv = m.Cash;
                sales.Balance = m.Balance;
                sales.Change_due = m.Return;
                sales.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                sales.CreateDate = DateTime.Now;



                db.POS_MasterSale.Add(sales);
            }
            db.SaveChanges();
            int msid = sales.ID_MS;
           

            foreach (var d in details)
            {
                POS_ItemOut item = new POS_ItemOut()
                {
                    ID_ItemIn_fk = d.items,
                    Name = d.itemname,
                    Price = d.totals,
                    Quantity = d.Qtys,
                    ID_MS = msid,
                    Org_Id= Convert.ToInt32(Session["Org_Code"]),
                    CreateDate=DateTime.Now,
                    
                };
                db.POS_ItemOut.Add(item);
            }

            db.SaveChanges();
            return sales.ID_MS;
          
        }




        public ActionResult POS()
        {
            var category = db.tbl_Category.Include(a => a.POS_ItemIn).Where(a=>a.IsActive == "Y").ToList();
            var productitem = db.POS_ItemIn.ToList();
            ViewBag.category = category;
            ViewBag.productitem = productitem;
            
            ViewBag.scriptBox = new SelectList(from ba in db.POS_ItemIn
                                                  orderby ba.Name
                                                   select new
                                                   {
                                                       id = ba.ID_ItemIn,
                                                       text = ba.Name + " @" + ba.RetailPrice + ""
                                                   }, "id", "text");
           

            ViewBag.list = new SelectList(from ba in db.POS_MasterSale
                                          orderby ba.ID_MS descending
                                          select new
                                          {
                                              id = ba.ID_MS,
                                              text = ba.MS_Code 
                                          }, "id", "text");


            ViewBag.CategoryID = new SelectList(db.tbl_Category.Where(a => a.IsActive == "Y"), "CategoryID", "Name");
            return View();
        }

        public string ItemSave(string Itemname, string price, int CategoryID)
        {

            POS_ItemIn tbl = new POS_ItemIn();
            tbl.CategoryID = CategoryID;
            tbl.Name = Itemname;
            tbl.RetailPrice =price;
            tbl.CreateDate = DateTime.Now;
            tbl.CreateBy = Session["name"].ToString();
            tbl.Org_Id = Convert.ToInt32(Session["Org_Code"]);
            db.POS_ItemIn.Add(tbl);
            db.SaveChanges();
            return "Data Saved" ;
        }

        public string ItemReturn(string ID, string Amount)
        {

            POS_ItemOut tbl = new POS_ItemOut();
            tbl.Name = "*Return";
            Amount = "-" + Amount;
            tbl.Price =Convert.ToInt32(Amount);
            
            tbl.ID_MS = Convert.ToInt32(ID);
            tbl.CreateDate = DateTime.Now;
            tbl.CreateBy = "Admin";
            tbl.Org_Id = Convert.ToInt32(Session["Org_Code"]);
            db.POS_ItemOut.Add(tbl);
            db.SaveChanges();
            int msid=Convert.ToInt32(ID);
            POS_MasterSale up = db.POS_MasterSale.Where(a => a.ID_MS == msid).FirstOrDefault();
            up.Net_Amount = up.Net_Amount + Convert.ToInt32(Amount);
            up.Change_due = up.Change_due - Convert.ToInt32(Amount);
           db.SaveChanges();

            return "Return Data Saved";
        }

        public ActionResult Index()
        {

             int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_SaleMaster.Include(t => t.tbl_Employee).Where(a => a.IsDelete == "No").OrderByDescending(a => a.SaleMst_ID).ToList());
            }     
            var tbl_salemaster = db.tbl_SaleMaster.Include(t => t.tbl_Employee).Where(a=>a.IsDelete=="No" && a.Org_Id == id).OrderByDescending(a=>a.SaleMst_ID);
            return View(tbl_salemaster.ToList());
        }

        //
        // GET: /SaleMaster/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_SaleMaster tbl_salemaster = db.tbl_SaleMaster.Find(id);
            if (tbl_salemaster == null)
            {
                return HttpNotFound();
            }
            return View(tbl_salemaster);
        }

        //
        // GET: /SaleMaster/Create

        public ActionResult Create()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            ViewBag.SaleType = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id), "SaleTypeID", "Name");
            ViewBag.InvoiceID = new SelectList(db.tbl_InvoiceInfo.Where(a => a.isactive == "Y"), "ID", "CompanyName");

         //   var q =  from d in Dealer
         //join dc in DealerConact on d.DealerID equals dc.DealerID
         //select dc;
            //ViewBag.Stock_ID = new SelectList(from q in db.tbl_SalesTypeSetupForm
            //                                  join d in db.tbl_Stock on q.SaleTypeID equals d.SaleTypeID 
            //                                  where q.Org_Id==id
            //                                  select new
            //{
            //    Stock_ID = d.Stock_ID,
            //    name= q.Name + Convert.ToString(d.CreateDate)
            //}, "Stock_ID", "name").Distinct();



            ViewBag.Stock_ID = new SelectList(db.tbl_SalesTypeSetupForm.Where(a => a.Org_Id == id), "SaleTypeID", "Name");
            ViewBag.EmpID = new SelectList(db.tbl_Employee.Where(a => a.Org_Id == id), "EmpID", "Name");
            return View();
        }
        public int GetProductPrice(int ProductId)
        {
            if (ProductId != 0)
            {

                return (int)db.tbl_Stock.Where(a => a.SaleTypeID == ProductId).OrderByDescending(a => a.Stock_ID).FirstOrDefault().Sale_Amount;
              //  return (int)db.tbl_Stock.Where(m => m.Stock_ID == ProductId && m.IsDelete == "N").FirstOrDefault().Sale_Amount;
            }
            else
            {
                return 0;
            }
        }

        //
        // POST: /SaleMaster/Create
         [HttpPost]
        public int Createe(string Products, int EmpID, decimal Total_Amount, string Description, string ApplyDate, string Discount, decimal Discount_Amount, int invoiceid)
      
        {

            tbl_SaleMaster saleMaster = new tbl_SaleMaster();
            int masterRecordId=0;

            using (TransactionScope tranScope = new TransactionScope())
            using (Entities_Data db = new Entities_Data())
            {
                try
                {


                    //UserID =userUserID .,
                    saleMaster.EmpID = EmpID;
                    saleMaster.GrossAmount = Total_Amount + Discount_Amount;
                    saleMaster.Discount__ = Discount;
                    saleMaster.Discount_Amount = Discount_Amount;
                    saleMaster.Total_Amount = Total_Amount;
                    saleMaster.Description = Description;
                    saleMaster.ApplyDate = Convert.ToDateTime(ApplyDate);
                    saleMaster.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                    saleMaster.CreateDate = DateTime.Now;
                    saleMaster.CreateBy = Session["name"].ToString();
                    saleMaster.UserID = Convert.ToInt32(Session["UserID"]);
                    saleMaster.invoiceid = invoiceid;
                    saleMaster.IsDelete = "No";
                    
                    db.tbl_SaleMaster.Add(saleMaster);
                    db.SaveChanges();
                    //Master sales ID
                    masterRecordId = saleMaster.SaleMst_ID;

                    dynamic products = JsonConvert.DeserializeObject(Products);
                    foreach (dynamic product in products)
                    {
                        int storedValue = product.Stock_ID.ToObject<int>();

                        // var SaleTypeID = db.tbl_Stock.Where(aa => aa.Stock_ID == storedValue).FirstOrDefault().SaleTypeID;
                        var stockidnew = db.tbl_Stock.Where(a => a.SaleTypeID == storedValue).OrderByDescending(a => a.Stock_ID).FirstOrDefault().Stock_ID;
                        var SaleTypeID = storedValue;
                        tbl_SaleDetail sale = new tbl_SaleDetail();
                        {

                            sale.ExpMstID = masterRecordId;
                            sale.Stock_ID = stockidnew;
                            sale.SaleTypeID = SaleTypeID;
                            sale.Stock_Retial_Price = product.Item_Price;
                            sale.Stock_Counts = product.Stock_Counts;
                            sale.Discount__ = product.Discount;
                            sale.Discount_Amount = product.Discount_Amount;
                            //Profit Calculate

                            var prof = db.tbl_Stock.Where(a => a.Stock_ID == stockidnew).FirstOrDefault().Unit_Amount ?? 0;
                            decimal profit = ((product.Item_Price - prof) * product.Stock_Counts);
                            sale.Profit_Amount = profit;
                            //
                            sale.Net_Sale_Price = product.Net_Price;
                            sale.Sale_Status = "S";
                            sale.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                            sale.CreateDate = DateTime.Now;
                            sale.CreateBy = Session["name"].ToString();
                            sale.UserID = Convert.ToInt32(Session["UserID"]);
                            sale.IsDelete = "No";

                        };
                        db.tbl_SaleDetail.Add(sale);
                        db.SaveChanges();
                    }

                    //for Accounts In Record
                    tbl_Account_Mst_Transaction acc = new tbl_Account_Mst_Transaction();
                    acc.Particular = Description;
                    acc.SaleMst_ID = masterRecordId;
                    acc.EmpID = EmpID;
                    acc.CR_Amount = Total_Amount;

                    acc.ActiveBalance = true;
                    var lastbalance = db.tbl_Account_Mst_Transaction.Where(a => a.EmpID == acc.EmpID && a.ActiveBalance == true).OrderByDescending(a => a.CreateDate).FirstOrDefault();


                    if (lastbalance == null)
                    {
                        acc.LastBalance = 0;
                        acc.Balance = 0 + Total_Amount;
                    }
                    else
                    {
                        acc.Balance = lastbalance.Balance + Total_Amount;
                        acc.LastBalance = lastbalance.Balance;
                    }

                    acc.IsDelete = "No";
                    acc.CreateDate = DateTime.Now;
                    acc.CreateBy = Session["name"].ToString();
                    acc.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                    acc.UserID = Convert.ToInt32(Session["UserID"]);
                    db.tbl_Account_Mst_Transaction.Add(acc);
                    db.SaveChanges();
                    
                    tranScope.Complete();
                }
                catch (Exception ex)
                {
                    
                    tranScope.Dispose();
                }
            }
            ///**** Code for Master Record ****/

            ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name", saleMaster.EmpID);
            return masterRecordId;

        }

         public ActionResult posprint(int saleMstId)
         {
             POS_MasterSale model = db.POS_MasterSale.Find(saleMstId);
             //tbl_SaleMst mstRecrod = db.tbl_SaleMst.Find(saleMstId);
             return View("Print", model);

         }



         public ActionResult PrintInvoice(int saleMstId)
         {
             tbl_SaleMaster model = db.tbl_SaleMaster.Find(saleMstId);
             int id = Convert.ToInt32(Session["Org_Code"]);
           var cr=  db.tbl_Account_Mst_Transaction.Where(a => a.IsDelete == "No" && a.EmpID == model.EmpID && a.Org_Id == id).Select(a=>a.CR_Amount).Sum();
           var dr = db.tbl_Account_Mst_Transaction.Where(a => a.IsDelete == "No" && a.EmpID == model.EmpID && a.Org_Id == id).Select(a => a.DR_Amount).Sum();
           int send = Convert.ToInt32(cr);
           int rcv =Convert.ToInt32(dr);
           ViewData["Balance"] = (send - rcv).ToString();
             //tbl_SaleMst mstRecrod = db.tbl_SaleMst.Find(saleMstId);
            return View("Invoice", model);
             

         }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(tbl_SaleMaster tbl_salemaster)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.tbl_SaleMaster.Add(tbl_salemaster);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name", tbl_salemaster.EmpID);
        //    return View(tbl_salemaster);
        //}

        //
        // GET: /SaleMaster/Edit/5

        public ActionResult Edit(int id = 0)
        {
            tbl_SaleMaster tbl_salemaster = db.tbl_SaleMaster.Find(id);
            if (tbl_salemaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name", tbl_salemaster.EmpID);
            return View(tbl_salemaster);
        }

        //
        // POST: /SaleMaster/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_SaleMaster tbl_salemaster)
        {
            if (ModelState.IsValid)
            {
                tbl_salemaster.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_salemaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpID = new SelectList(db.tbl_Employee, "EmpID", "Name", tbl_salemaster.EmpID);
            return View(tbl_salemaster);
        }

        //
        // GET: /SaleMaster/Delete/5

        public ActionResult Delete(int id = 0)
        {
            string msg;
            //Accounts delete
            tbl_Account_Mst_Transaction tbl_Account_Mst_Transaction = db.tbl_Account_Mst_Transaction.Where(a => a.SaleMst_ID == id).FirstOrDefault();
            db.tbl_Account_Mst_Transaction.Remove(tbl_Account_Mst_Transaction);
            db.SaveChanges();
            msg = "GL One Record Delete ";
            //Sale Details Delete
         
           var data= db.tbl_SaleDetail.Where(a => a.ExpMstID == id).ToList() ?? null;
           int c = 0;
           foreach (var i in data)
           {
               tbl_SaleDetail tbl_SaleDetail = db.tbl_SaleDetail.Find(i.SaleDetail_ID);
               db.tbl_SaleDetail.Remove(tbl_SaleDetail);
               db.SaveChanges();
               c++;
           }
           msg = (msg +"|"+ c.ToString() + " Records sale Details Deleted ").ToString();
           //Master Sale Delete      
            tbl_SaleMaster tbl_salemaster = db.tbl_SaleMaster.Find(id);
            db.tbl_SaleMaster.Remove(tbl_salemaster);
            db.SaveChanges();
            msg = msg + "|" + "(1)Record Invoice Deleted";
            ViewBag.delmsg = msg;
           return RedirectToAction("Index", "SaleMaster");
        }

        //
        // POST: /SaleMaster/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_SaleMaster tbl_salemaster = db.tbl_SaleMaster.Find(id);
            db.tbl_SaleMaster.Remove(tbl_salemaster);
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