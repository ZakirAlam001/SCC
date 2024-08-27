using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;
using System.Security.Principal;

namespace Data_Manager.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Start()
        {
            return View();
        }

        Entities_Data db = new Entities_Data();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string Username, string Password)
        {
            using (db)
            {
                tbl_User emp = db.tbl_User.Where(x => x.LoginID == Username && x.Password == Password && x.IsActive=="Y" ).FirstOrDefault();
                if (emp != null)
                {
                    Session["employee"] = emp;
                    Session["UserID"] = emp.UserID;
                    Session["name"] = emp.UserName;
                    Session["Org_Code"] = emp.Org_Id;


                    //string auth = "-N-Y".ToString();
                    String ThisMachine;
                    ThisMachine = WindowsIdentity.GetCurrent().Name.ToString();

                    if (ThisMachine == emp.SA1)
                    {
                        return RedirectToAction("Index", "Reports_Chart");
                    }
                    return Content("User authorization failed --------!  Illegal software use ErrorCode." + ThisMachine);
                   // return Response.Write("User Authrization failed");
                }

            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session["employee"] = null;
            return RedirectToAction("Index");
        }

    }
}
