using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using MultiPART.Services;
using MultiPART.UnitOfWork;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class ProcedureController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly ProjectUserValidationService _projectuservalidationservice;

        public ProcedureController()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                string[] roles = Roles.GetRolesForUser(membershipUser.UserName);
                var userKey = (int)membershipUser.ProviderUserKey;
                _projectuservalidationservice = new ProjectUserValidationService(new ModelStateWrapper(ModelState), _uow, userKey, roles);
            }
            else { throw new UnauthorizedAccessException("MembershipUser or ProviderUserKey is null - Is the user authenticated?"); }
        }

        //
        // GET: /Procedure/Create

        public ActionResult Create(int projectid = 0, string procedurepurpose = "")
        {

            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ViewBag.proceduretypelist = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Procedure Type" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            ViewBag.procedurepurpose = procedurepurpose;
            ViewBag.MultiPARTProjectID = projectid;

            ProcedureViewModel procedureVM = new ProcedureViewModel()
            {
                MultiPARTProjectID = projectid,
                ProcedurePurposeID = db.Options.Where(p => p.Status == "Current" && p.OptionFields.OptionFieldName == "procedurepurpose").Select(p => p.OptionID).FirstOrDefault(),
                ProcedurePurpose = procedurepurpose,
            };

            return View(procedureVM);
        }

        //
        // POST: /Procedure/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProcedureViewModel procedureVM)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.proceduretypelist = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Procedure Type" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

                ViewBag.procedurepurpose = procedureVM.ProcedurePurpose;
                ViewBag.MultiPARTProjectID = procedureVM.MultiPARTProjectID;

                return View(procedureVM);
            }

            if (procedureVM == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(procedureVM.ProcedureID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }


            Procedure procedure = new Procedure()
            {
                MultiPARTProjectMultiPARTProjectID = procedureVM.MultiPARTProjectID,
                ProcedureLabel = procedureVM.ProcedureLabel,
                ProcedureTypeID = procedureVM.ProcedureTypeID,
                ProcedurePurposeID = db.Options.Where(p => p.Status == "Current" && p.OptionFields.OptionFieldName == "Procedure Purpose" && p.OptionValue == procedureVM.ProcedurePurpose).Select(p => p.OptionID).FirstOrDefault(),
                Details = procedureVM.Details,

                CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                CreatedOn = DateTimeOffset.Now,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };

            db.Procedures.Add(procedure);
            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = procedureVM.MultiPARTProjectID });



        }


        //
        // GET: /Procedure/Edit/5

        public ActionResult Edit(int procedureid)
        {
            Procedure procedure = db.Procedures.Find(procedureid);

            if (procedure == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(procedure.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ProcedureViewModel procedureVM = new ProcedureViewModel()
            {
                ProcedureID = procedure.ProcedureID,
                ProcedureTypeID = procedure.ProcedureTypeID,
                ProcedureLabel = procedure.ProcedureLabel,
                MultiPARTProjectID = procedure.MultiPARTProjectMultiPARTProjectID,
                ProcedurePurposeID = procedure.ProcedureID,
                Details = procedure.Details,
                ProcedureTypeList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Procedure Type" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue")


            };

            return View(procedureVM);
        }

        //
        // POST: /Procedure/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProcedureViewModel procedureVM)
        {
            if (!ModelState.IsValid) 
            {
                        ViewBag.proceduretypelist = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Procedure Type" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            return View(procedureVM);
        }
            if (procedureVM == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(procedureVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            Procedure procedure = db.Procedures.Find(procedureVM.ProcedureID);

            procedure.ProcedureLabel = procedureVM.ProcedureLabel;
            procedure.ProcedureTypeID = procedureVM.ProcedureTypeID;
            procedure.MultiPARTProjectMultiPARTProjectID = procedureVM.MultiPARTProjectID;
            procedure.Details = procedureVM.Details;

            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = procedureVM.MultiPARTProjectID });



        }

        //
        // POST: /Procedure/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int procedureid)
        {
            Procedure procedure = db.Procedures.Find(procedureid);

            if (procedure == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(procedure.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }


            procedure.Status = "Deleted";
            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = procedure.MultiPARTProjectMultiPARTProjectID });
        }
    }
}
