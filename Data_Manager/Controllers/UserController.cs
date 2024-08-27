using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class UserController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /User/

        public ActionResult Index()
        {
            
            return View(db.tbl_User.ToList());
        }

        public ActionResult Datarefresh()
        {
            ViewBag.Org_Id = new SelectList(db.tbl_Orgcode, "Org_Id", "Name");
            string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection conn = new SqlConnection(con);
            SqlCommand cmd = new SqlCommand();
            //ViewBag.HRCo_ = new SelectList(db.HRMS_Company_ST, "HRCo_", "Title");

            try { 
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                cmd = new SqlCommand("[datarefresh]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                return View("Create");
            }
            }
            catch
            {
                return View("Create");
            }

            return View("Create");
        }



        //
        // GET: /User/Details/5

        public ActionResult Details(int id = 0)
        {
            tbl_User tbl_user = db.tbl_User.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.Org_Id = new SelectList(db.tbl_Orgcode, "Org_Id", "Name");
          //  TempData["sdata"]=TempData["UserName"];
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_User tbl_user)
        {
            ViewBag.Org_Id = new SelectList(db.tbl_Orgcode, "Org_Id", "Name", tbl_user.Org_Id);
            if (ModelState.IsValid)
            {
                tbl_user.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_user.CreateDate = DateTime.Now;
                tbl_user.CreateBy = Session["name"].ToString();
              
                db.tbl_User.Add(tbl_user);
                tbl_user.IsDelete = "N";
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_user);
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.Org_Id = new SelectList(db.tbl_Orgcode, "Org_Id", "Name");
            tbl_User tbl_user = db.tbl_User.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_User tbl_user)
        {
            ViewBag.Org_Id = new SelectList(db.tbl_Orgcode, "Org_Id", "Name", tbl_user.Org_Id);
            if (ModelState.IsValid)
            {
                tbl_user.UserID = Convert.ToInt32(Session["UserID"]);
                tbl_user.UpdateDate = DateTime.Now;
                tbl_user.UpdateBy = Session["name"].ToString();             
                db.Entry(tbl_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_user);
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tbl_User tbl_user = db.tbl_User.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_User tbl_user = db.tbl_User.Find(id);
            db.tbl_User.Remove(tbl_user);
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