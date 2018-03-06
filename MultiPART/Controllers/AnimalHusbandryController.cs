using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using MultiPART.Services;
using MultiPART.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class AnimalHusbandryController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly ProjectUserValidationService _projectuservalidationservice;

        public AnimalHusbandryController()
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
        // GET: /AnimalHusbandry/

        public ActionResult Index(int projectid = 0, int researchgroupid = 0, int strainid = 0)
        {
            if (!_projectuservalidationservice.UserIsInProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            int userid = (int)Membership.GetUser().ProviderUserKey;
            ViewBag.projectid = projectid;
            ViewBag.strainid = strainid;
            ViewBag.researchgroupid = researchgroupid;

            ViewBag.researchgroupList = new SelectList(db.Researchgroups.Where(r => r.Status == "Current"
                                          && r.UserInResearchgroups.Where(u => u.Status == "Current" && (u.EndTime == null || u.EndTime > DateTime.Now)).Select(u => u.UserProfileUserId).Contains(userid)
                                          && r.ResearchgroupInMultiPARTProjects.Where(rim => rim.Status == "Current").Select(rim => rim.MultiPARTProjectMultiPARTProjectID).Contains(projectid)
                                          ).AsEnumerable(), "ResearchgroupID", "ResearchgroupName", researchgroupid);

            var strain = from c in db.Cohorts
                         where c.Status == "Current" && c.MultiPARTProjectMultiPARTProjectID == projectid && c.MultiPARTProjects.Status == "Current" && c.Strain.Status == "Current"
                         select c.Strain;

            ViewBag.StrainList = new SelectList(strain.Distinct().AsEnumerable(), "StrainID", "StrainName");


            var cohorts = from c in db.Cohorts
                          where c.Status == "Current"
                          && c.MultiPARTProjectMultiPARTProjectID == projectid
                          && c.MultiPARTProjects.Status == "Current"
                          && c.ResearchgroupCohortAssignments.Where(rca => rca.Status == "Current").Select(r => r.ResearchgroupID).Contains(researchgroupid)
                          && c.StrainStrainID == strainid
                          select c;

            if (cohorts.Count() > 0)
            {

                var AnimalHusbandryVM = (from ahf in db.AnimalHusbandryFields.Where(f => f.Status == "Current")
                                         join ah in db.AnimalHusbandries.Where(a => a.Status == "Current" && a.MultiPARTProjectMultiPARTProjectID == projectid && a.StrainID == strainid && a.ResearchgroupResearchgroupID == researchgroupid)
                                         on ahf.AnimalHusbandryFieldID equals ah.AnimalHusbandryFieldID into ahlist
                                         from newah in ahlist.DefaultIfEmpty()
                                         select new { ahf, newah }).AsEnumerable()
                                        .Select(newah => new AnimalHusbandryViewModel
                                        {
                                            AnimalHusbandryID = (newah.newah == null) ? 0 : newah.newah.AnimalHusbandryID,

                                            MultiPARTProjectID = projectid,
                                            StrainID = strainid,
                                            ResearchgroupID = researchgroupid,

                                            FieldID = newah.ahf.AnimalHusbandryFieldID,
                                            FieldName = newah.ahf.AnimalHusbandryFieldName,
                                            FieldType = newah.ahf.Options.OptionValue,

                                            OptionID = (newah.newah == null) ? null : newah.newah.AnimalHusbandryOptionID,

                                            Value = (newah.newah == null) ? string.Empty : newah.newah.Value,

                                            Options = new SelectList(db.AnimalHusbandryOptions.Where(o => o.Status == "Current" && o.AnimalHusbandryFieldID == newah.ahf.AnimalHusbandryFieldID).AsEnumerable(), "AnimalHusbandryOptionID", "AnimalHusbandryOptionName"),


                                        });

                ViewBag.N = AnimalHusbandryVM.Count();

                return View(AnimalHusbandryVM);
            }
            else
            {
                ViewBag.N = -1;
                ViewBag.ErrorMessage = "No Cohort was found for this research group and strain. Cohort with strain input must be created before animal husbandry can be added";
                return View();
            }
        }

        //
        // POST: /AnimalHusbandry/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IList<AnimalHusbandryViewModel> AnimalHusbandryVM)
        {
            if (!ModelState.IsValid || AnimalHusbandryVM == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserIsInProject(AnimalHusbandryVM.FirstOrDefault().MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            int userid = (int)Membership.GetUser().ProviderUserKey;
            int projectid = AnimalHusbandryVM.FirstOrDefault().MultiPARTProjectID;
            int researchgroupid = AnimalHusbandryVM.FirstOrDefault().ResearchgroupID;
            int strainid = AnimalHusbandryVM.FirstOrDefault().StrainID;

            foreach (var ah in AnimalHusbandryVM)
            {
                if (ah.AnimalHusbandryID > 0)
                {
                    AnimalHusbandry AnimalHusbandry = db.AnimalHusbandries.Find(ah.AnimalHusbandryID);

                    if (AnimalHusbandry.AnimalHusbandryFieldID == ah.FieldID
                        && AnimalHusbandry.StrainID == ah.StrainID
                        && AnimalHusbandry.MultiPARTProjectMultiPARTProjectID == ah.MultiPARTProjectID
                        && AnimalHusbandry.ResearchgroupResearchgroupID == ah.ResearchgroupID)
                    {
                        AnimalHusbandry.AnimalHusbandryOptionID = ah.OptionID;
                        AnimalHusbandry.Value = ah.Value;
                        AnimalHusbandry.LastUpdatedBy = userid;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Some illegal changes had been made to the animal husbandry record. Please contact administrator about the error.";
                        return View("Error");
                    }
                }
                else if (ah.AnimalHusbandryID == 0)
                {
                    AnimalHusbandry AnimalHusbandry = new AnimalHusbandry()
                    {
                        AnimalHusbandryFieldID = ah.FieldID,
                        StrainID = ah.StrainID,
                        MultiPARTProjectMultiPARTProjectID = ah.MultiPARTProjectID,
                        ResearchgroupResearchgroupID = ah.ResearchgroupID,

                        LastUpdatedBy = userid,
                    };

                    AnimalHusbandry.AnimalHusbandryOptionID = ah.OptionID;
                    AnimalHusbandry.Value = ah.Value;

                    db.AnimalHusbandries.Add(AnimalHusbandry);
                }
                else
                {
                    ViewBag.ErrorMessage = "Some illegal changes had been made to the animal husbandry table. Please contact administrator for the error.";
                    return View("Error");
                }
            }


            db.SaveChanges();

            return RedirectToAction("Index", new { projectid = projectid, researchgroupid = researchgroupid, strainid = strainid });


            //   ViewBag.ErrorMessage = "Saving to table failed. Please seed help from the administrator. ";

            //return View("Error");



        }

    }
}
