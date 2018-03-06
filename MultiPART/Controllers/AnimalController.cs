using MultiPART.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MultiPART.Models.ViewModel;
using System.Web.Security;
using MultiPART.Models.Table;
using System.Web.Mvc;
using MultiPART.Services;
using MultiPART.UnitOfWork;
using MultiPART.Authorization;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class AnimalController : Controller
    {
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private MultipartContext db = new MultipartContext();
        private readonly DataEntryService _dataEntryService;
                private readonly ProjectUserValidationService _projectuservalidationservice;
                private readonly string _displaytimeunit = "Hour";


        public AnimalController()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                string[] roles = Roles.GetRolesForUser(membershipUser.UserName);
                var userKey = (int)membershipUser.ProviderUserKey;
                _dataEntryService = new DataEntryService(new ModelStateWrapper(ModelState), _uow, userKey);
                _projectuservalidationservice = new ProjectUserValidationService(new ModelStateWrapper(ModelState), _uow, userKey, roles);


            }
            else { throw new UnauthorizedAccessException("MembershipUser or ProviderUserKey is null - Is the user authenticated?"); }

            var DisplayTimeUnit = _displaytimeunit;

            var DisplayUnit = db.Units.Where(u => u.Status == "Current" && u.UnitName == DisplayTimeUnit);

            if (DisplayUnit.Count() != 1) throw new UnauthorizedAccessException("Time unit not recognized! Please contact administrator.");
        }
        //
        // GET: /Animal/

        public ActionResult Index(int projectid, int researchgroupid = 0, int diseasemodelinductionid = 0)
        {
            if (!(_projectuservalidationservice.UserCanCreateAnimal(projectid) || _projectuservalidationservice.UserCanEnterExperimentData(projectid)))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ViewBag.projectid = projectid;
            ViewBag.researchgroupid = researchgroupid;
            ViewBag.diseasemodelinductionid = diseasemodelinductionid;

            int userid = (int)Membership.GetUser().ProviderUserKey;

            ViewBag.researchgroupList = new SelectList(db.Researchgroups.Where(r => r.Status == "Current"
                  && r.UserInResearchgroups.Where(u => u.Status == "Current" && (u.EndTime == null || u.EndTime > DateTime.Now)).Select(u => u.UserProfileUserId).Contains(userid)
                  && r.ResearchgroupInMultiPARTProjects.Where(rim => rim.Status == "Current").Select(rim => rim.MultiPARTProjectMultiPARTProjectID).Contains(projectid)
                  ).AsEnumerable(), "ResearchgroupID", "ResearchgroupName", researchgroupid);

            var diseasemodel = from p in db.Procedures
                               where p.Status == "Current"
                               && p.MultiPARTProjectMultiPARTProjectID == projectid
                               && p.MultiPARTProjects.Status == "Current"
                               && p.OptionsProcedurePurpose.OptionValue == "Disease Model Induction"
                               orderby p.ProcedureLabel
                               select new ProcedureViewModel
                               {
                                   ProcedureID = p.ProcedureID,
                                   ProcedureLabel = p.ProcedureLabel,
                               };

            ViewBag.diseasemodelList = new SelectList(diseasemodel.AsEnumerable(), "ProcedureID", "ProcedureLabel", diseasemodelinductionid);

            var cohorts = (from c in db.Cohorts
                           where c.Status == "Current"
                           && c.MultiPARTProjectMultiPARTProjectID == projectid
                           && c.MultiPARTProjects.Status == "Current"
                           && c.ResearchgroupCohortAssignments.Where(rca => rca.Status == "Current").Select(r => r.ResearchgroupID).Contains(researchgroupid)
                           && c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current").Select(cpa => cpa.ProcedureID).Contains(diseasemodelinductionid)
                           select c).AsEnumerable()
            .Select(c => new CohortListViewModel
            {
                MultiPARTProjectID = c.MultiPARTProjectMultiPARTProjectID,
                CohortID = c.CohortID,
                CohortLabel = c.CohortLabel,
                NumberOfProcedures = c.CohortProcedureAssignments.Count(x => x.Status == "Current"),
                Cormobidity = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Comorbidity Induction").Select(cpa => cpa.Procedures.ProcedureLabel)),
                DiseaseModelInduction = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Disease Model Induction").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                Treatment = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Treatment").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                OutcomeAssessment = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Outcome Assessment").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
            });

            ViewBag.N = cohorts.Count();

            ViewBag.researchrole =
                db.UserProjectAssignments.Where(upa => upa.Status == "Current"
                    && upa.UserInResearchgroups.UserProfileUserId == userid
                    && upa.UserInResearchgroups.ResearchgroupResearchgroupID == researchgroupid
                    && upa.ResearchgroupInMultiPARTProjects.Status == "Current"
                    && upa.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID == projectid
                    && upa.ResearchgroupInMultiPARTProjects.ResearchgroupResearchgroupID == researchgroupid
                    ).Select(upa => upa.Options.OptionValue).ToList();

            return View(cohorts);
        }

        //
        // GET: /AnimalList/
        public ActionResult AnimalList(int projectid, int researchgroupid, int diseasemodelinductionid)
        {
            if (!_projectuservalidationservice.UserCanCreateAnimal(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ViewBag.projectid = projectid;
            ViewBag.researchgroupid = researchgroupid;
            ViewBag.diseasemodelinductionid = diseasemodelinductionid;

            var cohorts = (from c in db.Cohorts
                           where c.Status == "Current"
                           && c.MultiPARTProjectMultiPARTProjectID == projectid
                           && c.MultiPARTProjects.Status == "Current"
                           && c.ResearchgroupCohortAssignments.Where(rca => rca.Status == "Current").Select(r => r.ResearchgroupID).Contains(researchgroupid)
                           && c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current").Select(cpa => cpa.ProcedureID).Contains(diseasemodelinductionid)
                           select c).AsEnumerable()
           .Select(c => new CohortListViewModel
           {
               MultiPARTProjectID = c.MultiPARTProjectMultiPARTProjectID,
               CohortID = c.CohortID,
               CohortLabel = c.CohortLabel,
               NumberOfProcedures = c.CohortProcedureAssignments.Count(x => x.Status == "Current"),
               Cormobidity = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Comorbidity Induction").Select(cpa => cpa.Procedures.ProcedureLabel)),
               DiseaseModelInduction = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Disease Model Induction").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
               Treatment = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Treatment").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
               OutcomeAssessment = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Outcome Assessment").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
               NumberOfAnimals = c.Animals.Count(a => a.Status == "Current" && a.ResearchgroupID == researchgroupid),
           });

            ViewBag.cohorts = cohorts;
            List<int> cohortids = cohorts.Select(c => c.CohortID).ToList();

            var animalVM = (from a in db.Animals
                            where a.Status == "Current" && a.ResearchgroupID == researchgroupid
                            && cohortids.Contains(a.CohortID)
                            orderby a.CreatedOn
                            select a).AsEnumerable()
                           .Select(a => new AnimalListViewModel
                           {
                               AnimalID = a.AnimalID,
                               AnimalLabel = a.AnimalLabel,
                               Cohort = a.Cohort.CohortLabel,
                               Treatment = String.Join(", ", a.Cohort.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Treatment")
                               .Select(cpa => cpa.Procedures.ProcedureLabel))

                           });

            return View(animalVM);
        }

        //Data entry form list
        // GET: /AnimalList2/
        public ActionResult AnimalList2(int projectId, int researchGroupId, int diseaseModelInductionId)
        {
            if (!_projectuservalidationservice.UserCanEnterExperimentData(projectId))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            ViewBag.projectid = projectId;
            ViewBag.researchgroupid = researchGroupId;
            ViewBag.diseasemodelinductionid = diseaseModelInductionId;

            var DisplayTimeUnit = _displaytimeunit;
            ViewBag.DisplayTimeUnit = DisplayTimeUnit;

            var DisplayUnitConversionFactor = db.Units.FirstOrDefault(u => u.Status == "Current" && u.UnitName == DisplayTimeUnit).ConversionFactor;

            var cohortids = from c in db.Cohorts
                            where c.Status == "Current"
                            && c.MultiPARTProjectMultiPARTProjectID == projectId
                            && c.MultiPARTProjects.Status == "Current"
                            && c.ResearchgroupCohortAssignments.Where(rca => rca.Status == "Current").Select(r => r.ResearchgroupID).Contains(researchGroupId)
                            && c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current").Select(cpa => cpa.ProcedureID).Contains(diseaseModelInductionId)
                            orderby c.CohortLabel
                            select c.CohortID;

            var animalVM = (from a in db.Animals.Where(a => a.Status == "Current" && a.ResearchgroupID == researchGroupId && cohortids.Contains(a.CohortID))
                            orderby a.CreatedOn
                            select a).AsEnumerable()
                           .Select(a => new AnimalListExperimenterViewModel
                           {
                               AnimalID = a.AnimalID,
                               AnimalLabel = a.AnimalLabel,
                               Procedures = a.Cohort.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current").Select(cpa => new ProcedureViewModel
                               {
                                   ProcedureID = cpa.ProcedureID,
                                   ProcedureLabel = cpa.Procedures.ProcedureLabel,
                                   ProcedurePurpose = cpa.Procedures.OptionsProcedurePurpose.OptionValue,
                                   Administrations = cpa.Procedures.Administrations.Select(ad => new AdministrationViewModel
                                   { 
                                      AdministrationID = ad.AdministrationID,
                                      StartTime = ad.StartTime / DisplayUnitConversionFactor,
                                      EndTime = ad.EndTime / DisplayUnitConversionFactor,
                                      Entered = (_dataEntryService.GetAnimalAdministration(a.AnimalID, ad.AdministrationID) != null),
                                   }).ToList(),
                               }).ToList(),

                               ProjectID = projectId,
                               ResearchGroupID = researchGroupId,
                               DiseaseModelInductionId = diseaseModelInductionId
                           });

            ViewBag.PageTitle = db.MultiPARTProjects.Find(projectId).MultiPARTProjectName;

            return View(animalVM.ToList());
        }

        //
        // GET: /Randomization/Create

        public ActionResult Create(int researchgroupid, int diseasemodelinductionid, int projectid)
        {
            if (!_projectuservalidationservice.UserCanCreateAnimal(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            AnimalViewModel animalVM = new AnimalViewModel
            {
                ResearchgroupID = researchgroupid,
                ProjectID = projectid,
                DiseasseModelInductionID = diseasemodelinductionid,

            };

            return View(animalVM);
        }

        //
        // POST: /Randomization/Create

        [HttpPost]
        public ActionResult Create(AnimalViewModel animalVM)
        {
            if (!ModelState.IsValid || animalVM == null) View(animalVM);

            if (!_projectuservalidationservice.UserCanCreateAnimal(animalVM.ProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

                Animal animal = new Animal
                {
                    AnimalLabel = animalVM.AnimalLabel,
                    ResearchgroupID = animalVM.ResearchgroupID,

                    CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                    CreatedOn = DateTimeOffset.Now,
                    LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
                };

                var cohorts = from r in db.ResearchgroupCohortAssignments
                              orderby r.CohortID
                              where r.Status == "Current"
                              && r.Cohorts.Status == "Current"
                              && r.ResearchgroupID == animalVM.ResearchgroupID
                              && r.Researchgroups.Status == "Current"
                              && r.Cohorts.MultiPARTProjectMultiPARTProjectID == animalVM.ProjectID
                              && r.Cohorts.MultiPARTProjects.Status == "Current"
                              && r.Cohorts.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current").Select(cpa => cpa.ProcedureID).Contains(animalVM.DiseasseModelInductionID)
                              select new RandomizationViewModel
                              {
                                  CohortLabel = r.Cohorts.CohortLabel,
                                  CohortID = r.CohortID,
                                  CurrentNumberOfAnimals = r.Cohorts.Animals.Count(a => a.ResearchgroupID == animalVM.ResearchgroupID),
                                  TotalNumberOfAnimals = r.NumberOfAnimals,
                                  
                              };

                
                animal.CohortID = RandomisationService.RandomizeAnimal(cohorts.ToList());

                db.Animals.Add(animal);

                db.SaveChanges();

                return RedirectToAction("Details", new { animalid = animal.AnimalID, researchgroupid = animalVM.ResearchgroupID, diseasemodelinductionid = animalVM.DiseasseModelInductionID });

        }

        //
        // GET: /Randomization/Details/5

        public ActionResult Details(int animalid, int researchgroupid, int diseasemodelinductionid)
        {
            Animal animal = db.Animals.Find(animalid);

            if (animal == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanCreateAnimal(animal.Cohort.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

                AnimalViewModel animalVM = new AnimalViewModel
                {
                    AnimalID = animal.AnimalID,
                    AnimalLabel = animal.AnimalLabel,
                    CohortLabel = animal.Cohort.CohortLabel,
                    ProjectID = animal.Cohort.MultiPARTProjectMultiPARTProjectID,
                    ResearchgroupID = researchgroupid,
                    DiseasseModelInductionID = diseasemodelinductionid
                };

                ViewBag.projectid = animalVM.ProjectID;
                ViewBag.researchgroupid = researchgroupid;
                ViewBag.diseasemodelinductionid = diseasemodelinductionid;
                return View(animalVM);


        }

    }
}
