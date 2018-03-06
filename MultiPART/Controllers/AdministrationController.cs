using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiPART.Models.Table;
using MultiPART.Models;
using MultiPART.Models.ViewModel;
using MultiPART.Authorization;
using MultiPART.UnitOfWork;
using MultiPART.Services;
using System.Web.Security;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class AdministrationController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly ProjectUserValidationService _projectuservalidationservice;
        private readonly string _displaytimeunit = "Hour";

        //
        // GET: /Administration/

        public AdministrationController()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                string[] roles = Roles.GetRolesForUser(membershipUser.UserName);
                var userKey = (int)membershipUser.ProviderUserKey;
                _projectuservalidationservice = new ProjectUserValidationService(new ModelStateWrapper(ModelState), _uow, userKey, roles);
            }
            else { throw new UnauthorizedAccessException("MembershipUser or ProviderUserKey is null - Is the user authenticated?"); }

            var DisplayTimeUnit = _displaytimeunit;

            var DisplayUnit = db.Units.Where(u => u.Status == "Current" && u.UnitName == DisplayTimeUnit);

            if (DisplayUnit.Count() != 1) throw new UnauthorizedAccessException("Time unit not recognized! Please contact administrator.");
        }

        public ActionResult Index(int procedureid, int projectid)
        {
            var DisplayTimeUnit = _displaytimeunit;
            ViewBag.DisplayTimeUnit = DisplayTimeUnit;

            var DisplayUnitConversionFactor = db.Units.Where(u => u.Status == "Current" && u.UnitName == DisplayTimeUnit).FirstOrDefault().ConversionFactor;

            var access = _projectuservalidationservice.GetUserProjectAccessType(projectid);

            if (access == AssessType.None)
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var procedure = db.Procedures.Find(procedureid);
            ViewBag.procedurelabel = procedure.ProcedureLabel;

            var administrationVM = db.Administrations.Where(a => a.Status == "Current" && a.ProcedureID == procedureid && a.Procedure.Status == "Current")
                .Select(a => new AdministrationViewModel
                {
                    AdministrationID = a.AdministrationID,
                    ProjectID = projectid,
                    ProcedureID = a.ProcedureID,
                    StartTime = a.StartTime / DisplayUnitConversionFactor,
                    EndTime = a.EndTime / DisplayUnitConversionFactor,
                }).ToList();

            if (access == AssessType.Edit)
            {
                ViewBag.procedureid = procedureid;
                ViewBag.projectid = projectid;

                if (access == AssessType.Edit) return View(administrationVM);
            }

            if (access == AssessType.View) return View("IndexDisplay", administrationVM);

            return View("Error");

        }

        //
        // GET: /Administration/Create

        public ActionResult Create(int procedureid, int projectid)
        {
           if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

                ViewBag.ProcedureID = new SelectList(db.Procedures, "ProcedureID", "ProcedureLabel");

                AdministrationViewModel administrationVM = new AdministrationViewModel
                {
                    ProcedureID = procedureid,
                    unitList = new SelectList(db.Units.Where(u => u.Status == "Current" && u.SIUnite.QuantityName == "Time").AsEnumerable(), "UnitID", "UnitSymbol"),

                };

                ViewBag.projectid = projectid;
                ViewBag.procedureid = procedureid;

                return View(administrationVM);
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdministrationViewModel administrationVM, int projectid)
        {
             if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }
                if (ModelState.IsValid)
                {
                    var unit = db.Units.Find(administrationVM.unitID);

                    var factor = unit.ConversionFactor;

                    Administration administration = new Administration
                    {
                        StartTime = Convert.ToInt32(administrationVM.StartTime*factor),
                        EndTime = Convert.ToInt32(administrationVM.EndTime*factor),
                        ProcedureID = administrationVM.ProcedureID,
                        CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,               
                    };

                    db.Administrations.Add(administration);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { procedureid = administrationVM.ProcedureID, projectid = administrationVM.ProjectID });
                }
                return View(administrationVM);
        }


        //  POST: /Administration/Index

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IEnumerable<AdministrationViewModel> administrationVMs, int projectid)
        {
            var DisplayTimeUnit = _displaytimeunit;
            ViewBag.DisplayTimeUnit = DisplayTimeUnit;

            var DisplayUnitConversionFactor = db.Units.Where(u => u.Status == "Current" && u.UnitName == DisplayTimeUnit).FirstOrDefault().ConversionFactor;

            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                foreach (var administrationVM in administrationVMs)
                {
                    Administration administration = db.Administrations.Find(administrationVM.AdministrationID);

                    if (administration != null)
                    {
                        if (administration.Status == "Current")
                        {
                            administration.StartTime = Convert.ToInt32(administrationVM.StartTime * DisplayUnitConversionFactor);
                            administration.EndTime = Convert.ToInt32(administrationVM.EndTime * DisplayUnitConversionFactor);
                        }
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index", new { procedureid = administrationVMs.FirstOrDefault().ProcedureID, projectid = administrationVMs.FirstOrDefault().ProjectID });
            }

            return View(administrationVMs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(FormCollection formCollection, int projectid, int procedureid, int administrationid, int starttime, int endtime)
        {
            if (formCollection == null)
            {
                ViewBag.ErrorMessage = "No data submitted.";

                return View("Error");
            }

            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                Administration administration = db.Administrations.Find(administrationid);

                if (administration != null)
                {
                    if (administration.Status == "Current")
                    {
                        administration.StartTime = starttime;
                        administration.EndTime = endtime;

                        administration.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;
                    }
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index", new { procedureid = procedureid, projectid = projectid });
        }

        //
        // POST: /Administration/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int administrationid, int procedureid, int projectid)
        {
            if (_projectuservalidationservice.UserCanEditProject(projectid))
            {
                Administration administration = db.Administrations.Find(administrationid);
                if (administration != null)
                {
                    administration.Status = "Deleted";

                    db.SaveChanges();
                }
                return RedirectToAction("Index", new { procedureid = procedureid, projectid = projectid });
            }

            ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
            return View("Error");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}