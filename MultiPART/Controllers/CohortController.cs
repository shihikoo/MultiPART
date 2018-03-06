using MultiPART.Models;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using MultiPART.Models.LinkTable;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.Entity;
using MultiPART.Authorization;
using MultiPART.UnitOfWork;
using MultiPART.Services;
using System;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class CohortController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly ProjectUserValidationService _projectuservalidationservice;

        public CohortController()
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
        // GET: /Cohort/Create

        public ActionResult Create(int projectid = 0)
        {
            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ViewBag.StrainList = new SelectList(db.Strains.Where(p => p.Status == "Current").AsEnumerable(), "StrainID", "StrainName");
            ViewBag.SexList = new SelectList(db.Options.Where(c => c.Status == "Current" && c.OptionFields.OptionFieldName == "Sex").AsEnumerable(), "OptionID", "OptionValue");

            ViewBag.MultiPARTProjectID = projectid;

            CohortViewModel cohortVM = new CohortViewModel()
            {
                MultiPARTProjectID = projectid
            };

            return View(cohortVM);
        }

        //
        // POST: /Cohort/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CohortViewModel cohortVM)
        {
            if (!ModelState.IsValid || cohortVM == null) return View(cohortVM);

            if (!_projectuservalidationservice.UserCanEditProject(cohortVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            Cohort cohort = new Cohort()
            {
                MultiPARTProjectMultiPARTProjectID = cohortVM.MultiPARTProjectID,
                CohortLabel = cohortVM.CohortLabel,

                StrainStrainID = cohortVM.StrainID,
                SampleSize = cohortVM.SampleSize ?? 0,
                SexID = cohortVM.SexID,
                MinAge = cohortVM.MinAge ?? 0,
                MaxAge = cohortVM.MaxAge ?? 0,
                MinWeight = cohortVM.MinWeight ?? 0,
                MaxWeight = cohortVM.MaxWeight ?? 0,
                Details = cohortVM.Details,

                CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };

            db.Cohorts.Add(cohort);
            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = cohortVM.MultiPARTProjectID });
        }

        //
        // GET: /Cohort/Edit/5

        public ActionResult Edit(int cohortid)
        {
            Cohort cohort = db.Cohorts.Find(cohortid);

            if (cohort == null) return new HttpStatusCodeResult(422);
            if (cohort.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(cohort.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            CohortViewModel cohortVM = new CohortViewModel()
            {
                CohortID = cohort.CohortID,
                MultiPARTProjectID = cohort.MultiPARTProjectMultiPARTProjectID,
                CohortLabel = cohort.CohortLabel,
                StrainID = cohort.StrainStrainID,
                SampleSize = cohort.SampleSize,
                SexID = cohort.SexID,
                MinAge = cohort.MinAge,
                MaxAge = cohort.MaxAge,
                MinWeight = cohort.MinWeight,
                MaxWeight = cohort.MaxWeight,
                Details = cohort.Details,

            };

            ViewBag.StrainList = new SelectList(db.Strains.Where(p => p.Status == "Current").AsEnumerable(), "StrainID", "StrainName");
            ViewBag.SexList = new SelectList(db.Options.Where(c => c.Status == "Current" && c.OptionFields.OptionFieldName == "Sex").AsEnumerable(), "OptionID", "OptionValue");
            ViewBag.MultiPARTProjectID = cohort.MultiPARTProjectMultiPARTProjectID;

            return View(cohortVM);
        }

        //
        // POST: /Cohort/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CohortViewModel cohortVM)
        {
            if (cohortVM == null) return new HttpStatusCodeResult(422);

            if (!ModelState.IsValid)
            {
                ViewBag.StrainList = new SelectList(db.Strains.Where(p => p.Status == "Current").AsEnumerable(), "StrainID", "StrainName");
                ViewBag.SexList = new SelectList(db.Options.Where(c => c.Status == "Current" && c.OptionFields.OptionFieldName == "Sex").AsEnumerable(), "OptionID", "OptionValue");
                ViewBag.MultiPARTProjectID = cohortVM.MultiPARTProjectID;

                return View(cohortVM);
            }

            if (!_projectuservalidationservice.UserCanEditProject(cohortVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            Cohort cohort = new Cohort()
            {
                CohortID = cohortVM.CohortID,
                MultiPARTProjectMultiPARTProjectID = cohortVM.MultiPARTProjectID,
                CohortLabel = cohortVM.CohortLabel,
                StrainStrainID = cohortVM.StrainID,
                SampleSize = cohortVM.SampleSize ?? 0,
                SexID = cohortVM.SexID,
                MinAge = cohortVM.MinAge ?? 0,
                MaxAge = cohortVM.MaxAge ?? 0,
                MinWeight = cohortVM.MinWeight ?? 0,
                MaxWeight = cohortVM.MaxWeight ?? 0,
                Details = cohortVM.Details,

                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };
            db.Entry(cohort).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = cohortVM.MultiPARTProjectID });


        }

        //
        // POST: /Cohort/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int cohortid)
        {
            Cohort cohort = db.Cohorts.Find(cohortid);

            if (cohort == null) return new HttpStatusCodeResult(422);
            if (cohort.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(cohort.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            cohort.Status = "Deleted";
            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = cohort.MultiPARTProjectMultiPARTProjectID });
        }

        //
        // GET: /Cohort/AddResearchgroup?cohortid=5
        public ActionResult IndexResearchgroup(int cohortid)
        {
            Cohort cohort = db.Cohorts.Find(cohortid);

            if (cohort == null) return new HttpStatusCodeResult(422);
            if (cohort.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(cohort.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }
            var rcaVM = from rca in db.ResearchgroupCohortAssignments
                        where rca.Status == "Current" && rca.CohortID == cohortid && rca.Cohorts.Status == "Current" && rca.Researchgroups.Status == "Current"
                        select new ResearchgroupCohortAssignmentListViewModel
                        {
                            MultiPARTProjectID = rca.Cohorts.MultiPARTProjectMultiPARTProjectID,
                            ResearchgroupName = rca.Researchgroups.ResearchgroupName,
                            NumberOfAnimals = rca.NumberOfAnimals,
                            ResearchgroupCohortAssignmentID = rca.ResearchgroupCohortAssignmentID,

                        };

            if (rcaVM == null) return View("Index", "Home");

            ViewBag.MultiPARTProjectID = cohort.MultiPARTProjectMultiPARTProjectID;
            ViewBag.CohortLabel = cohort.CohortLabel;
            ViewBag.cohortid = cohortid;
            return View(rcaVM);
        }

        //
        // GET: /Cohort/AddResearchgroup?cohortid=5

        public ActionResult AddResearchgroup(int cohortid)
        {
            Cohort cohort = db.Cohorts.Find(cohortid);

            if (cohort == null) return new HttpStatusCodeResult(422);
            if (cohort.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(cohort.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var existingRGIDs = db.ResearchgroupCohortAssignments.Where(rca => rca.Status == "Current" && rca.CohortID == cohortid).Select(rca=>rca.ResearchgroupID);

            ViewBag.ResearchgroupList = new SelectList(
                db.Researchgroups.Where(r => db.ResearchgroupInMultiPARTProjects.Where(rm => rm.Status == "Current"
                    && rm.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID
                    
                    && !existingRGIDs.Contains(rm.ResearchgroupResearchgroupID)
                    && rm.Options.OptionValue == "Wet Lab").Select(rm => rm.ResearchgroupResearchgroupID).Contains(r.ResearchgroupID)).AsEnumerable(),
                    "ResearchgroupID", "ResearchgroupName");

            ViewBag.cohortid = cohortid;
            ViewBag.cohortlabel = cohort.CohortLabel;

            ResearchgroupCohortAssignmentViewModel rcaVM = new ResearchgroupCohortAssignmentViewModel
            {
                CohortID = cohortid,
                MultiPARTProjectID = cohort.MultiPARTProjectMultiPARTProjectID,
                CohortLabel = cohort.CohortLabel
            };

            return View(rcaVM);
        }

        //
        // POST: /Cohort/AddResearchgroup?cohortid=5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResearchgroup(ResearchgroupCohortAssignmentViewModel rcaVM)
        {
            if (!ModelState.IsValid || rcaVM == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(rcaVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ResearchgroupCohortAssignment rca = new ResearchgroupCohortAssignment()
            {
                ResearchgroupID = rcaVM.ResearchgroupID,
                NumberOfAnimals = rcaVM.NumberOfAnimals,
                CohortID = rcaVM.CohortID,

                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                CreatedBy = (int)Membership.GetUser().ProviderUserKey,

            };

            db.ResearchgroupCohortAssignments.Add(rca);

            db.SaveChanges();
            return RedirectToAction("IndexResearchgroup", new { cohortid = rcaVM.CohortID });
        }

        //
        // GET: /Cohort/Details/5

        public ActionResult EditResearchgroup(int rcaid)
        {
            ResearchgroupCohortAssignment rca = db.ResearchgroupCohortAssignments.Find(rcaid);

            if (rca == null) return new HttpStatusCodeResult(422);
            if (rca.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(rca.Cohorts.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ViewBag.ResearchgroupList = new SelectList(
                db.Researchgroups.Where(r => db.ResearchgroupInMultiPARTProjects.Where(rm => rm.Status == "Current"
                    && rm.MultiPARTProjectMultiPARTProjectID == rca.Cohorts.MultiPARTProjectMultiPARTProjectID
                    && rm.Options.OptionValue == "Wet Lab").Select(rm => rm.ResearchgroupResearchgroupID).Contains(r.ResearchgroupID)).AsEnumerable(),
                    "ResearchgroupID", "ResearchgroupName", rca.ResearchgroupID);

            ViewBag.cohortid = rca.CohortID;

            ResearchgroupCohortAssignmentViewModel rcaVM = new ResearchgroupCohortAssignmentViewModel()
            {
                CohortID = rca.CohortID,
                MultiPARTProjectID = rca.Cohorts.MultiPARTProjectMultiPARTProjectID,
                ResearchgroupID = rca.ResearchgroupID,
                ResearchgroupName = rca.Researchgroups.ResearchgroupName,
                NumberOfAnimals = rca.NumberOfAnimals,
                ResearchgroupCohortAssignmentID = rca.ResearchgroupCohortAssignmentID,
            };

            ViewBag.MultiPARTProjectID = rca.Cohorts.MultiPARTProjectMultiPARTProjectID;

            return View(rcaVM);
        }

        //
        // POST: /Cohort/EditResearchgroup?cohortid=5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResearchgroup(ResearchgroupCohortAssignmentViewModel rcaVM)
        {
            if (!ModelState.IsValid || rcaVM == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(rcaVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ResearchgroupCohortAssignment rca = db.ResearchgroupCohortAssignments.Find(rcaVM.ResearchgroupCohortAssignmentID);

       //     rca.ResearchgroupID = rcaVM.ResearchgroupID;
            rca.NumberOfAnimals = rcaVM.NumberOfAnimals;

            rca.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.SaveChanges();
            return RedirectToAction("IndexResearchgroup", new { cohortid = rcaVM.CohortID });
        }

        //
        // POST: /Cohort/EditResearchgroup?cohortid=5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResearchgroup(int rcaid)
        {
            ResearchgroupCohortAssignment rca = db.ResearchgroupCohortAssignments.Find(rcaid);

            if (rca == null) return new HttpStatusCodeResult(422);
            if (rca.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(rca.Cohorts.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            rca.Status = "Deleted";
            rca.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.SaveChanges();

            return RedirectToAction("IndexResearchgroup", new { cohortid = rca.CohortID });
        }

        //
        // GET: /Cohort/AddResearchgroup?cohortid=5

        public ActionResult AddProcedure(int cohortid)
        {
            Cohort cohort = db.Cohorts.Find(cohortid);

            if (cohort == null) return new HttpStatusCodeResult(422);
            if (cohort.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(cohort.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var diseasemodelIDs = db.CohortProcedureAssignments.Where(x => x.Status == "Current" && x.CohortID == cohortid && x.Procedures.Status == "Current" && x.Procedures.OptionsProcedurePurpose.OptionValue == "Disease Model Induction").Select(l => l.ProcedureID).FirstOrDefault();

            var comorbidityIDs = db.CohortProcedureAssignments.Where(x => x.Status == "Current" && x.CohortID == cohortid && x.Procedures.Status == "Current" && x.Procedures.OptionsProcedurePurpose.OptionValue == "Comorbidity Induction").Select(l => l.ProcedureID);

            var treatmentIDs = db.CohortProcedureAssignments.Where(x => x.Status == "Current" && x.CohortID == cohortid && x.Procedures.Status == "Current" && x.Procedures.OptionsProcedurePurpose.OptionValue == "Treatment").Select(l => l.ProcedureID);

            var outcomeIDs = db.CohortProcedureAssignments.Where(x => x.Status == "Current" && x.CohortID == cohortid && x.Procedures.Status == "Current" && x.Procedures.OptionsProcedurePurpose.OptionValue == "Outcome Assessment").Select(l => l.ProcedureID);

            var anesthesiaIDs = db.CohortProcedureAssignments.Where(x => x.Status == "Current" && x.CohortID == cohortid && x.Procedures.Status == "Current" && x.Procedures.OptionsProcedurePurpose.OptionValue == "Anaesthesia").Select(l => l.ProcedureID);

            var analgesiaIDs = db.CohortProcedureAssignments.Where(x => x.Status == "Current" && x.CohortID == cohortid && x.Procedures.Status == "Current" && x.Procedures.OptionsProcedurePurpose.OptionValue == "Post-Operative Analgesia").Select(l => l.ProcedureID);


            ViewBag.DiseaseModelids = new SelectList(db.Procedures
                .Where(r => r.Status == "Current" && r.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID && r.OptionsProcedurePurpose.OptionValue == "Disease Model Induction").AsEnumerable()
                , "ProcedureID", "ProcedureLabel", diseasemodelIDs);

            ViewBag.Comorbidityids = new MultiSelectList(db.Procedures
              .Where(r => r.Status == "Current" && r.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID && r.OptionsProcedurePurpose.OptionValue == "Comorbidity Induction").AsEnumerable()
              , "ProcedureID", "ProcedureLabel", comorbidityIDs);

            ViewBag.Treatmentids = new MultiSelectList(db.Procedures
             .Where(r => r.Status == "Current" && r.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID && r.OptionsProcedurePurpose.OptionValue == "Treatment").AsEnumerable()
             , "ProcedureID", "ProcedureLabel", treatmentIDs);

            ViewBag.OutcomeAssessmentids = new MultiSelectList(db.Procedures
             .Where(r => r.Status == "Current" && r.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID && r.OptionsProcedurePurpose.OptionValue == "Outcome Assessment").AsEnumerable()
             , "ProcedureID", "ProcedureLabel", outcomeIDs);

            ViewBag.Anesthesiaids = new MultiSelectList(db.Procedures
          .Where(r => r.Status == "Current" && r.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID && r.OptionsProcedurePurpose.OptionValue == "Anaesthesia").AsEnumerable()
          , "ProcedureID", "ProcedureLabel", anesthesiaIDs);

            ViewBag.Analgesiaids = new MultiSelectList(db.Procedures
          .Where(r => r.Status == "Current" && r.MultiPARTProjectMultiPARTProjectID == cohort.MultiPARTProjectMultiPARTProjectID && r.OptionsProcedurePurpose.OptionValue == "Post-Operative Analgesia").AsEnumerable()
          , "ProcedureID", "ProcedureLabel", analgesiaIDs);

            ViewBag.MultiPARTProjectID = cohort.MultiPARTProjectMultiPARTProjectID;
            ViewBag.cohortid = cohortid;

            return View();
        }

        //
        // POST: /Cohort/AddResearchgroup?cohortid=5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProcedure(List<int> Comorbidity, List<int> DiseaseModel, List<int> Treatment, List<int> OutcomeAssessment, List<int> Anaesthesia, List<int> Analgesia, int cohortid, int projectid)
        {
            Cohort cohort = db.Cohorts.Find(cohortid);

            if (cohort == null) return new HttpStatusCodeResult(422);
            if (cohort.Status != "Current") return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            List<CohortProcedureAssignment> oldcpq = db.CohortProcedureAssignments.Where(c => c.CohortID == cohortid).ToList();

            foreach (var old in oldcpq)
            {
                old.Status = "Delete";
            }
            if (Comorbidity != null)
            {
                foreach (int newid in Comorbidity)
                {
                    CohortProcedureAssignment cpa = new CohortProcedureAssignment()
                    {
                        CohortID = cohortid,
                        ProcedureID = newid,

                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    };

                    db.CohortProcedureAssignments.Add(cpa);
                }
            }

            if (DiseaseModel != null)
            {
                foreach (int newid in DiseaseModel)
                {
                    CohortProcedureAssignment cpa = new CohortProcedureAssignment()
                    {
                        CohortID = cohortid,
                        ProcedureID = newid,

                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    };

                    db.CohortProcedureAssignments.Add(cpa);
                }
            }

            if (Treatment != null)
            {
                foreach (int newid in Treatment)
                {
                    CohortProcedureAssignment cpa = new CohortProcedureAssignment()
                    {
                        CohortID = cohortid,
                        ProcedureID = newid,

                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    };
                    db.CohortProcedureAssignments.Add(cpa);
                }
            }

            if (OutcomeAssessment != null)
            {
                foreach (int newid in OutcomeAssessment)
                {
                    CohortProcedureAssignment cpa = new CohortProcedureAssignment()
                    {
                        CohortID = cohortid,
                        ProcedureID = newid,

                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    };

                    db.CohortProcedureAssignments.Add(cpa);
                }
            }

            if (Anaesthesia != null)
            {
                foreach (int newid in Anaesthesia)
                {
                    CohortProcedureAssignment cpa = new CohortProcedureAssignment()
                    {
                        CohortID = cohortid,
                        ProcedureID = newid,

                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    };

                    db.CohortProcedureAssignments.Add(cpa);
                }
            }

            if (Analgesia != null)
            {
                foreach (int newid in Analgesia)
                {
                    CohortProcedureAssignment cpa = new CohortProcedureAssignment()
                    {
                        CohortID = cohortid,
                        ProcedureID = newid,

                        LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                    };

                    db.CohortProcedureAssignments.Add(cpa);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Details", "MultiPARTProject", new { projectid = projectid });
        }

    }
}
