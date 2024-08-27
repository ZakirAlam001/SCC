using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class AppsFeedbackController : Controller
    {
        private Entities_Data db = new Entities_Data();

        //
        // GET: /AppsFeedback/

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["Org_Code"]);
            if (id == 1)
            {
                return View(db.tbl_Complain_apps.ToList());
            }

            return View(db.tbl_Complain_apps.Where(a => a.Org_Id == id).ToList());
        
        }

        //
        // GET: /AppsFeedback/Details/5

        public ActionResult Details(decimal id = 0)
        {
            tbl_Complain_apps tbl_complain_apps = db.tbl_Complain_apps.Find(id);
            if (tbl_complain_apps == null)
            {
                return HttpNotFound();
            }
            return View(tbl_complain_apps);
        }

        //
        // GET: /AppsFeedback/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AppsFeedback/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Complain_apps tbl_complain_apps)
        {
            if (ModelState.IsValid)
            {
                tbl_complain_apps.UserID = Convert.ToInt32(Session["UserID"]);
               
                tbl_complain_apps.IsDelete = "N";
                tbl_complain_apps.CreateDate = DateTime.Now;
                tbl_complain_apps.CreateBy = Session["name"].ToString();
                tbl_complain_apps.Status = "Pending.";
                tbl_complain_apps.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.tbl_Complain_apps.Add(tbl_complain_apps);
                db.SaveChanges();
                #region body
                string id = "gmail030392";
                #endregion
                var aa = db.tbl_Orgcode.Where(a => a.Org_Id == tbl_complain_apps.Org_Id).FirstOrDefault().email;
                // Console.WriteLine("Mail To");
              //  MailAddress to="abc@abc.com";
                
                MailAddress to = new MailAddress("muhammad.zakir@esquare.com.pk");
               // }

                // Console.WriteLine("Mail From");
                MailAddress from = new MailAddress("AppsComplain@bitzenta.com", "AppsComplain@bitzenta.com");

                MailMessage mail = new MailMessage(from, to);
                if (aa != null)
                {
                    string ab = aa;
                    mail.Bcc.Add(aa);
                }
                // Console.WriteLine("Subject");
                mail.Subject = "Your Complain Successfully Submit Complain ID : " + tbl_complain_apps.complain_ID + "";
                //  Console.WriteLine("Your Message");
                mail.Sender = new MailAddress("Order@bitzenta.com", "Bitzenta");
                mail.Body = "<div style='background: #ebeef1;padding: 20px; border-radius: 50px;border: 1px solid #34abb8;border-top: 50px solid #34abb8;'> Dear <b style='color : #3498db'> Admin </b><br/> <h2 style='color : #3498db'>Your Complain Successfully Submit Complain ID : " + tbl_complain_apps.complain_ID + "</h2><br/>  Complain Id : <b style='color : #3498db'> " + tbl_complain_apps.complain_ID + "</b> <br/>Message By : " + Session["name"].ToString() + "<br/> Org ID : (" + Session["Org_Code"].ToString() + ")<br/> Message : <sub style='color:red'> " + tbl_complain_apps.Coment_Text + "</sub><br/> <br/><br/><br/>System Generated Email By Bitzenta do not reply  <hr> www.bitzenta.com <br/> Zakir Alam <br/> 0 334 3787191 <br/> zakir@bitzenta.com <br/> Karachi, Pakistan <br><img width='200' src='http://www.bitzenta.com/images/logo.png'></div>";

                mail.IsBodyHtml = true;


                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("namezakir@gmail.com", id);
                // Console.WriteLine("Sending email...");
                smtp.Send(mail);
                TempData["msg"] = "<script>alert('Your Order ID Successfully Send To Email Please Check Your Email !'); window.location.href = 'http://www.bitzenta.com';</script>";
               
                return RedirectToAction("Index");
            }

            return View(tbl_complain_apps);
        }

        //
        // GET: /AppsFeedback/Edit/5

        public ActionResult Edit(decimal id = 0)
        {
            tbl_Complain_apps tbl_complain_apps = db.tbl_Complain_apps.Find(id);
            if (tbl_complain_apps == null)
            {
                return HttpNotFound();
            }
            return View(tbl_complain_apps);
        }

        //
        // POST: /AppsFeedback/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_Complain_apps tbl_complain_apps)
        {
            if (ModelState.IsValid)
            {
                tbl_complain_apps.IsDelete = "N";
                tbl_complain_apps.UpdateDate = DateTime.Now;
                tbl_complain_apps.UpdateBy = Session["name"].ToString();
                tbl_complain_apps.Status = "Pending.";
                tbl_complain_apps.Org_Id = Convert.ToInt32(Session["Org_Code"]);
                db.Entry(tbl_complain_apps).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_complain_apps);
        }

        //
        // GET: /AppsFeedback/Delete/5

        public ActionResult Delete(decimal id = 0)
        {
            tbl_Complain_apps tbl_complain_apps = db.tbl_Complain_apps.Find(id);
            if (tbl_complain_apps == null)
            {
                return HttpNotFound();
            }
            return View(tbl_complain_apps);
        }

        //
        // POST: /AppsFeedback/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            tbl_Complain_apps tbl_complain_apps = db.tbl_Complain_apps.Find(id);
            db.tbl_Complain_apps.Remove(tbl_complain_apps);
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