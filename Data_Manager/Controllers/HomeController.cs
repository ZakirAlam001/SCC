using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Manager.Models;

namespace Data_Manager.Controllers
{
    public class HomeController : Controller
    {   
        //MedicalClaimEntities db = new MedicalClaimEntities();
        //Employee loggedInEmployee = (Employee)System.Web.HttpContext.Current.Session["employee"];

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }
        //[HttpGet]
        //public ActionResult Claim() //Claim List
        //{
        //    ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
        //    return View(GetClaimsByEmployeeId(loggedInEmployee.EmployeeId));
        //}

        //[HttpPost] 
        //public ActionResult Claim(MedicalClaim claim) //Insert
        //{
        //    claim.EmployeeId = loggedInEmployee.EmployeeId;
        //    claim.Status = "Pending";
        //    claim.ClaimDate = DateTime.Now;
        //    db.MedicalClaims.Add(claim);
        //    db.SaveChanges();

        //    return Json((from obj in db.MedicalClaims.Where(m => m.EmployeeId == loggedInEmployee.EmployeeId && m.isDelete == false).OrderByDescending(m => m.ClaimDate)
        //                 select new
        //                 {
        //                     claimId = obj.ClaimId,
        //                     ClaimType = obj.ClaimType,
        //                     ClaimAmount = obj.ClaimAmount,
        //                     Hospital = obj.Hospital,
        //                     ClaimDate = obj.ClaimDate,
        //                     Details = obj.Details,
        //                     Status = obj.Status
        //                 }), JsonRequestBehavior.AllowGet);            
        //}

        //[HttpPost]
        //public ActionResult UpdateClaim(MedicalClaim claim) //Update /Edit
        //{
        //    ViewBag.test = "update claim";

        //    MedicalClaim xClaim = db.MedicalClaims.Find(claim.ClaimId);
        //    xClaim.ClaimType = claim.ClaimType;
        //    xClaim.ClaimAmount = claim.ClaimAmount;
        //    xClaim.Hospital = claim.Hospital;
        //    xClaim.Details = claim.Details;
        //    xClaim.lastUpdate = DateTime.Now;
        //    db.SaveChanges();            
        //    return Json((from obj in db.MedicalClaims.Where(m => m.EmployeeId == loggedInEmployee.EmployeeId && m.isDelete == false).OrderByDescending(m => m.ClaimDate)
        //                 select new
        //                 {
        //                     claimId = obj.ClaimId,
        //                     ClaimType = obj.ClaimType,
        //                     ClaimAmount = obj.ClaimAmount,
        //                     Hospital = obj.Hospital,
        //                     ClaimDate = obj.ClaimDate,
        //                     Details = obj.Details,
        //                     Status = obj.Status
        //                 }), JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult DeleteClaim(int deleteId)  // Delete
        //{
        //    ViewBag.test = "delete claim";

        //    MedicalClaim claim = db.MedicalClaims.Find(deleteId);
        //    claim.isDelete = true;
        //    claim.actionById = loggedInEmployee.EmployeeId;
        //    claim.actionDate = DateTime.Now;
        //    db.SaveChanges();
        //    return Json(new { id = deleteId }, JsonRequestBehavior.AllowGet);            
        //}

        //private List<MedicalClaim> GetClaimsByEmployeeId(int employeeId)
        //{
        //    var claims = db.MedicalClaims.Where(x => x.EmployeeId == employeeId && x.isDelete == false).OrderByDescending(x => x.ClaimDate).ToList();
        //    return claims;
        //}

    }
}
