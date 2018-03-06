using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using System.Web.Security;
using MultiPART.Models.LinkTable;
using MultiPART.UnitOfWork;
using MultiPART.Services;

namespace MultiPART.Controllers
{
    [Authorize]
    [Restrict("Disabled")]
    public class MultiPARTProjectController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly ProjectUserValidationService _projectuservalidationservice;

        public MultiPARTProjectController()
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
        // GET: /MultiPARTProject/

        public ActionResult Index(string sortOrder = "ProjectStartDate desc")
        {
            var Projects = from p in db.MultiPARTProjects.Include(p => p.Files)
                           where p.Status == "Current"
                           orderby p.MultiPARTProjectName
                           select new MultiPARTProjectListViewModel
                           {
                               MultiPARTProjectID = p.MultiPARTProjectID,
                               MultiPARTProjectName = p.MultiPARTProjectName,
                               ProjectStartDate = p.ProjectStartDate,
                               ProjectCompletionDateExpected = p.ProjectCompletionDateExpected,
                               Objectives = p.Objectives,
                               ProjectComletionDate = p.ProjectComletionDate,
                               Logo = p.Files.FileUrl,
                           };

            int userid = (int)Membership.GetUser().ProviderUserKey;

            var reserachgroupIDs = (from r in db.UserInResearchgroups.Include(uir => uir.Researchgroups)
                                    where r.Status == "Current" && (r.EndTime == null || r.EndTime > DateTime.Now) && r.UserProfileUserId == userid && r.Researchgroups.Status == "Current"
                                    select r.ResearchgroupResearchgroupID).ToList();

            ViewBag.haveresearchgroup = reserachgroupIDs.Count() > 0;

            // administrator can see all project
            if (User.IsInRole("Administrator"))
            {
                ViewBag.Message = "You are allowed to view all projects in the database.";
            }
            else if (User.IsInRole("Superuser"))  //supser user can see every project in their group
            {
                ViewBag.Message = "You are allowed to view all projects in the database.";

            }
            else if (User.IsInRole("Poweruser"))  //supser user can see every project in their group
            {
                var projectIDs = (from p in db.ResearchgroupInMultiPARTProjects
                                  where p.Status == "Current" && reserachgroupIDs.Contains(p.ResearchgroupResearchgroupID) && p.MultiPartProject.Status == "Current"
                                  select p.MultiPARTProjectMultiPARTProjectID).ToList();

                Projects = Projects.Where(p => projectIDs.Contains(p.MultiPARTProjectID));

                ViewBag.Message = "If there is some project you would like to see is not listed here, please contact the Pincepal Investigator of that project to add your research group to that project. ";
            }
            else
            {
                var projectIDs = from p in db.UserProjectAssignments
                                 where p.Status == "Current" && p.UserInResearchgroups.UserProfileUserId == userid
                                 select p.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID;

                Projects = Projects.Where(p => projectIDs.Contains(p.MultiPARTProjectID));

                ViewBag.Message = "If there is some project you would like to see is not listed here, please contact the Pincepal Investigator of that project to add you to the project.";
            }

            switch (sortOrder)
            {
                case "MultiPARTProjectName":
                    Projects = Projects.OrderBy(s => s.MultiPARTProjectName);
                    break;
                case "MultiPARTProjectName desc":
                    Projects = Projects.OrderByDescending(s => s.MultiPARTProjectName);
                    break;
                case "ProjectStartDate":
                    Projects = Projects.OrderBy(s => s.ProjectStartDate);
                    break;
                case "ProjectStartDate desc":
                    Projects = Projects.OrderByDescending(s => s.ProjectStartDate);
                    break;
                case "ProjectCompletionDateExpected":
                    Projects = Projects.OrderBy(s => s.ProjectCompletionDateExpected);
                    break;
                case "ProjectCompletionDateExpected desc":
                    Projects = Projects.OrderByDescending(s => s.ProjectCompletionDateExpected);
                    break;
                case "ProjectComletionDate":
                    Projects = Projects.OrderBy(s => s.ProjectComletionDate);
                    break;
                case "ProjectComletionDate desc":
                    Projects = Projects.OrderByDescending(s => s.ProjectComletionDate);
                    break;
                default:
                    Projects = Projects.OrderByDescending(s => s.ProjectStartDate);
                    break;
            }

            ViewBag.sortOrder = sortOrder;
            ViewBag.NoProjectCreationMessage = "You currently do not have right to create project. If you are not within any research group, please contact your group leader. If you are the group leader. Please contact administrator.";

            return View(Projects.ToList());
        }

        //
        // GET: /MultiPARTProject/Details/5

        public ActionResult Details(int projectid = 0)
        {
            var access = _projectuservalidationservice.GetUserProjectAccessType(projectid);

            if (access == AssessType.None)
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var project = Project(projectid);

            if (access == AssessType.Edit) return View(project);
            if (access == AssessType.View) return View("ViewDetails", project);

            return View("Error");
        }

        private ProjectViewModel Project(int projectid)
        {
            var multipartproject = db.MultiPARTProjects.Find(projectid);
            return new ProjectViewModel()
            {
                ProjectId = multipartproject.MultiPARTProjectID,
                ProjectName = multipartproject.MultiPARTProjectName,
                EthicalStatement = multipartproject.EthicalStatement,
                StartDate = multipartproject.ProjectStartDate,
                ExpectedCompletionDate = multipartproject.ProjectCompletionDateExpected,
                CompletionDate = multipartproject.ProjectComletionDate,
                Background = multipartproject.Background,
                Objectives = multipartproject.Objectives,
                ResearchGroups = GetResearchGroupsInProject(projectid),
                Researchers = GetResearchers(projectid),
                Comorbidities = GetProcedure(projectid, "Comorbidity Induction"),
                DiseaseModels = GetProcedure(projectid, "Disease Model Induction"),
                Treatments = GetProcedure(projectid, "Treatment"),
                OutcomeAssessments = GetProcedure(projectid, "Outcome Assessment"),
                Anesthesia = GetProcedure(projectid, "Anaesthesia"),
                PostOpAnalgesia = GetProcedure(projectid, "Post-Operative Analgesia"),
                MortalityReport = GetProcedure(projectid, "Mortality Report"),
                Cohorts = GetCohorts(projectid),
                CohortProcedures = GetCohortProcedures(projectid),
                CohortResearchGroups = GetCohortResearchGroups(projectid)
            };
        }

        private IQueryable<ProcedureListViewModel> GetProcedure(int projectid, string procedureType)
        {
            return from p in db.Procedures
                   where p.Status == "Current" && p.MultiPARTProjectMultiPARTProjectID == projectid && p.OptionsProcedurePurpose.OptionValue == procedureType
                   orderby p.ProcedureLabel
                   select new ProcedureListViewModel
                   {
                       ProcedureID = p.ProcedureID,
                       ProcedureLabel = p.ProcedureLabel,
                       ProcedurePurpose = p.OptionsProcedurePurpose.OptionValue,
                       ProcedureType = p.OptionsProcedureType.OptionValue,
                       Details = p.Details,
                       NumberOfDetails = p.ProcedureDetails.Count(pd => pd.Status == "Current"),
                       NumberOfAdministration = p.Administrations.Count(pd => pd.Status == "Current"),
                       NumberOfFormField = p.DataEntryDesigns.Count(pd => pd.Status == "Current"),
                       MultiPARTProjectMultiPARTProjectID = p.MultiPARTProjectMultiPARTProjectID
                   };
        }

        private IEnumerable<CohortListViewModel> GetCohortResearchGroups(int projectid)
        {
            return (from c in db.Cohorts
                    where c.Status == "Current" && c.MultiPARTProjectMultiPARTProjectID == projectid
                    orderby c.CohortLabel
                    select c).AsEnumerable()
                .Select(c => new CohortListViewModel
                {
                    CohortID = c.CohortID,
                    CohortLabel = c.CohortLabel,
                    NumberOfResearchgroups = c.ResearchgroupCohortAssignments.Count(x => x.Status == "Current"),
                    Researchgroup = String.Join(", ", c.ResearchgroupCohortAssignments.Where(cpa => cpa.Status == "Current" && cpa.Researchgroups.Status == "Current").Select(cpa => cpa.Researchgroups.ResearchgroupName + " (" + cpa.NumberOfAnimals.ToString() + ")")),
                });
        }

        private IEnumerable<CohortProcedureListViewModel> GetCohortProcedures(int projectid)
        {
            return (from c in db.Cohorts
                    where c.Status == "Current" && c.MultiPARTProjectMultiPARTProjectID == projectid
                    orderby c.CohortLabel
                    select c).AsEnumerable()
                .Select(c => new CohortProcedureListViewModel
                {
                    CohortID = c.CohortID,
                    CohortLabel = c.CohortLabel,
                    NumberOfProcedures = c.CohortProcedureAssignments.Count(x => x.Status == "Current"),
                    Cormobidity = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Comorbidity Induction").Select(cpa => cpa.Procedures.ProcedureLabel)),
                    DiseaseModelInduction = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Disease Model Induction").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                    Treatment = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Treatment").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                    OutcomeAssessment = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Outcome Assessment").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                    Anaesthesia = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Anaesthesia").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                    Analgesia = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Post-Operative Analgesia").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                    MortalityReport = String.Join(", ", c.CohortProcedureAssignments.Where(cpa => cpa.Status == "Current" && cpa.Procedures.Status == "Current" && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Mortality Report").Select(cpa => cpa.Procedures.ProcedureLabel).ToList()),
                });
        }

        private IQueryable<UserProjectAssignmentListViewModel> GetResearchers(int projectid)
        {
            return from up in db.UserProjectAssignments
                   where up.Status == "Current" && up.UserInResearchgroups.Status == "Current" && (up.UserInResearchgroups.EndTime == null || up.UserInResearchgroups.EndTime > DateTime.Now)
                         && up.ResearchgroupInMultiPARTProjects.Status == "Current"
                         && up.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID == projectid && up.ResearchgroupInMultiPARTProjects.MultiPartProject.Status == "Current"
                   orderby up.UserInResearchgroups.UserProfiles.SurName ascending, up.UserInResearchgroups.UserProfiles.ForeName ascending
                   select new UserProjectAssignmentListViewModel
                   {
                       UserProjectAssignmentID = up.UserProjectAssignmentID,
                       Name = up.UserInResearchgroups.UserProfiles.ForeName + " " + up.UserInResearchgroups.UserProfiles.SurName,
                       UserRoleinProject = up.Options.OptionValue,
                       Researchgroup = up.ResearchgroupInMultiPARTProjects.Researchgroups.ResearchgroupName,
                       Institution = up.ResearchgroupInMultiPARTProjects.Researchgroups.Institutions.InstitutionName,
                   };
        }

        //
        // GET: /Cohort/Create

        public ActionResult CreateCohortModal(int projectid = 0)
        {
            //ViewBag.ResearchgroupList = new SelectList(db.Researchgroups.Where(p => p.Status == "Current").AsEnumerable(), "ResearchgroupID", "ResearchgroupName");
            ViewBag.StrainList = new SelectList(db.Strains.Where(p => p.Status == "Current").AsEnumerable(), "StrainID", "StrainName");
            ViewBag.SexList = new SelectList(db.Options.Where(c => c.Status == "Current" && c.OptionFields.OptionFieldName == "Sex").AsEnumerable(), "OptionID", "OptionValue");

            ViewBag.MultiPARTProjectID = projectid;

            CohortViewModel cohortVM = new CohortViewModel()
            {
                MultiPARTProjectID = projectid
            };

            return PartialView("_CreateCohortModal", cohortVM);
        }

        //
        // POST: /Cohort/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCohortModal(CohortViewModel cohortVM)
        {
            if (!ModelState.IsValid || cohortVM == null) return new HttpStatusCodeResult(422);

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
            return PartialView("_Cohorts", GetCohorts(cohortVM.MultiPARTProjectID));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CopyCohort(int cohortid, int projectid)
        {
            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            Cohort oldCohort = db.Cohorts.Find(cohortid);

            if (oldCohort != null)
            {
                Cohort cohort = new Cohort()
                {
                    MultiPARTProjectMultiPARTProjectID = oldCohort.MultiPARTProjectMultiPARTProjectID,
                    CohortLabel = "Copy of " + oldCohort.CohortLabel,

                    StrainStrainID = oldCohort.StrainStrainID,
                    SampleSize = oldCohort.SampleSize,
                    SexID = oldCohort.SexID,
                    MinAge = oldCohort.MinAge,
                    MaxAge = oldCohort.MaxAge,
                    MinWeight = oldCohort.MinWeight,
                    MaxWeight = oldCohort.MaxWeight,
                    Details = oldCohort.Details,

                    LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
                };

                db.Cohorts.Add(cohort);
                db.SaveChanges();
            }
            return PartialView("_Cohorts", GetCohorts(projectid));

        }

        [ChildActionOnly]
        public ActionResult CreateProcedureModal(int projectid = 0, string procedurepurpose = "")
        {

            var project = db.MultiPARTProjects.Find(projectid);
            var procedurePurposeId =
                db.Options.Where(o => o.Status == "Current" && o.OptionValue == procedurepurpose)
                    .Select(o => o.OptionID)
                    .FirstOrDefault();

            ProcedureViewModel procedureVM = new ProcedureViewModel()
            {
                ProjectName = project.MultiPARTProjectName,
                MultiPARTProjectID = projectid,
                ProcedurePurposeID = procedurePurposeId,
                ProcedurePurpose = procedurepurpose,
                ProcedureTypeList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Procedure Type" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue")
            };

            return PartialView("_CreateProcedureModal", procedureVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProcedureModal(ProcedureViewModel procedureVM)
        {
            if (!ModelState.IsValid)
            {
                procedureVM.ProcedureTypeList =
                new SelectList(
                    db.Options.Where(o => o.OptionFields.OptionFieldName == "Procedure Type" && o.Status == "Current")
                        .AsEnumerable(), "OptionID", "OptionValue");

                return RedirectToAction("Details", "MultiPARTProject", new { projectid = procedureVM.MultiPARTProjectID });
            }

            if (procedureVM == null) return new HttpStatusCodeResult(422);

            if (!_projectuservalidationservice.UserCanEditProject(procedureVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }


            Procedure procedure = new Procedure()
            {
                MultiPARTProjectMultiPARTProjectID = procedureVM.MultiPARTProjectID,
                ProcedureLabel = procedureVM.ProcedureLabel,
                ProcedureTypeID = procedureVM.ProcedureTypeID,
                ProcedurePurposeID = procedureVM.ProcedurePurposeID,
                Details = procedureVM.Details,
                CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                CreatedOn = DateTimeOffset.Now,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };

            db.Procedures.Add(procedure);
            db.SaveChanges();
            return PartialView("_ProcedureList",
                GetProcedure(procedureVM.MultiPARTProjectID, procedureVM.ProcedurePurpose));
        }

        public IEnumerable<CohortListViewModel> GetCohorts(int projectid)
        {
            return from c in db.Cohorts
                   where c.Status == "Current" && c.MultiPARTProjectMultiPARTProjectID == projectid
                   orderby c.CohortID
                   select new CohortListViewModel
                   {
                       CohortID = c.CohortID,
                       CohortLabel = c.CohortLabel,
                       Details = c.Details,
                       MultiPARTProjectID = projectid,
                       SampleSize = c.SampleSize,
                       NumberOfAnimals = (c.ResearchgroupCohortAssignments.Any(r => r.Status == "Current")) ? (c.ResearchgroupCohortAssignments.Where(r => r.Status == "Current").Select(r => r.NumberOfAnimals).Sum()) : 0,
                       Strain = c.Strain.StrainName,
                       Sex = c.OptionsSex.OptionValue,
                       MinWeight = c.MinWeight,
                       MaxWeight = c.MaxWeight
                   };
        }

        private IEnumerable<ResearchgroupInMultiPARTProjectViewModel> GetResearchGroupsInProject(int projectid)
        {
            var project = db.MultiPARTProjects.Find(projectid);
            return (from rgInp in db.ResearchgroupInMultiPARTProjects.Include("Researchgroups")
                   where rgInp.Status == "Current" && rgInp.Researchgroups.Status == "Current" && rgInp.MultiPARTProjectMultiPARTProjectID == projectid && rgInp.MultiPartProject.Status == "Current"
                   orderby rgInp.Researchgroups.ResearchgroupName
                   select rgInp).AsEnumerable()
                   .Select( rgInp =>  new ResearchgroupInMultiPARTProjectViewModel
                   {
                       ResearchgroupInMultiPARTProjectID = rgInp.ResearchgroupInMultiPARTProjectID,
                       MultiPARTProjectID = rgInp.MultiPARTProjectMultiPARTProjectID,
                       ResearchgroupID = rgInp.ResearchgroupResearchgroupID,
                       ResearchgroupName = rgInp.Researchgroups.ResearchgroupName + ", " + rgInp.Researchgroups.Institutions.InstitutionName + ", " + rgInp.Researchgroups.Institutions.Countries.CountryName,
                       RegistrationDate = rgInp.RegistrationDate,
                       ResearchgroupRoleinMultiPARTProject = rgInp.Options.OptionValue,
                       ProjectName = project.MultiPARTProjectName,
                       Editable = !_projectuservalidationservice.ResearchgroupAssignedCohort(rgInp.ResearchgroupResearchgroupID, rgInp.MultiPARTProjectMultiPARTProjectID),
                   });
        }

        //
        // GET: /MultiPARTProject/Create
        [Authorize(Roles = "Administrator, Superuser, Poweruser")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MultiPARTProject/Create
        [Authorize(Roles = "Administrator, Superuser, Poweruser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MultiPARTProjectViewModel multipartprojectVM)
        {
            int userid = (int)Membership.GetUser().ProviderUserKey;

            var userinresearchgroups = from r in db.UserInResearchgroups
                                       where r.Status == "Current" && r.UserProfileUserId == userid && r.Researchgroups.Status == "Current"
                                       select r;

            if (userinresearchgroups.Count() == 0)
            {
                ViewBag.ErrorMessage = "Access Denied. You do not belong to any reserch group. Please contact your group lead to add you into your group.";
                return View("Error");
            }


            if (!ModelState.IsValid) return View(multipartprojectVM);

            MultiPARTProject multipartproject = new MultiPARTProject()
            {
                MultiPARTProjectName = multipartprojectVM.MultiPARTProjectName,
                EthicalStatement = multipartprojectVM.EthicalStatement,
                ProjectStartDate = multipartprojectVM.ProjectStartDate,
                ProjectCompletionDateExpected = multipartprojectVM.ProjectCompletionDateExpected,
                Background = multipartprojectVM.Background,
                Objectives = multipartprojectVM.Objectives,

                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey

            };

            db.MultiPARTProjects.Add(multipartproject);



            foreach (var userinresearchgroup in userinresearchgroups)
            {
                ResearchgroupInMultiPARTProject researchgroupInMultiPARTProject = new ResearchgroupInMultiPARTProject
            {
                MultiPartProject = multipartproject,
                ResearchgroupResearchgroupID = userinresearchgroup.ResearchgroupResearchgroupID,
                ResearchgroupRoleinMultiPARTProjectID = db.Options.Where(o => o.OptionValue == "Wet Lab").FirstOrDefault().OptionID,

                RegistrationDate = DateTime.Now,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,

            };

                db.ResearchgroupInMultiPARTProjects.Add(researchgroupInMultiPARTProject);


                UserProjectAssignment userprojectassignment = new UserProjectAssignment
                {
                    ResearchgroupInMultiPARTProjects = researchgroupInMultiPARTProject,
                    UserInResearchgroupUserInResearchgroupID = userinresearchgroup.UserInResearchgroupID,
                    UserRoleinProjectID = db.Options.Where(o => o.OptionValue == "Principal Investigator").FirstOrDefault().OptionID,

                    LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey,
                };

                db.UserProjectAssignments.Add(userprojectassignment);
            }

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //
        // GET: /MultiPARTProject/Edit?projectid=1
        public ActionResult Edit(int projectid = 0)
        {
            MultiPARTProject multipartproject = db.MultiPARTProjects.Find(projectid);

            if (multipartproject == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var projectVm = new MultiPARTProjectViewModel()
            {
                MultiPARTProjectID = multipartproject.MultiPARTProjectID,
                MultiPARTProjectName = multipartproject.MultiPARTProjectName,
                EthicalStatement = multipartproject.EthicalStatement,
                ProjectStartDate = multipartproject.ProjectStartDate,
                ProjectCompletionDateExpected = multipartproject.ProjectCompletionDateExpected,
                ProjectComletionDate = multipartproject.ProjectComletionDate,
                Objectives = multipartproject.Objectives,
                Background = multipartproject.Background
            };

            return View(projectVm);
        }

        //
        // POST: /MultiPARTProject/Edit?projectid=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MultiPARTProjectViewModel projectVm)
        {
            if (!ModelState.IsValid) return View(projectVm);

            if (projectVm == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(projectVm.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            MultiPARTProject multipartproject = db.MultiPARTProjects.Find(projectVm.MultiPARTProjectID);

            multipartproject.MultiPARTProjectName = projectVm.MultiPARTProjectName;
            multipartproject.EthicalStatement = projectVm.EthicalStatement;
            multipartproject.ProjectStartDate = projectVm.ProjectStartDate;
            multipartproject.ProjectCompletionDateExpected = projectVm.ProjectCompletionDateExpected;
            multipartproject.ProjectComletionDate = projectVm.ProjectComletionDate;
            multipartproject.Background = projectVm.Background;
            multipartproject.Objectives = projectVm.Objectives;

            multipartproject.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.Entry(multipartproject).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { projectid = projectVm.MultiPARTProjectID });
        }

        //
        // POST: /MultiPARTProject/Delete?projectid=1
        [Authorize(Roles = "Administrator, Superuser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProject(int projectid)
        {
            MultiPARTProject multipartproject = db.MultiPARTProjects.Find(projectid);

            multipartproject.Status = "Deleted";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /MultiPARTProject/AddResearchgroupToProject
        public ActionResult AddResearchgroupToProject(int projectid = 0)
        {
            if (!_projectuservalidationservice.UserCanEditProject(projectid))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var ResearchgroupList = db.Researchgroups
               .Where(x => x.Status == "Current")
               .ToList()
               .OrderBy(c => c.ResearchgroupName)
               .Select(s => new
               {
                   ResearchgroupID = s.ResearchgroupID,
                   Researchgroup = string.Format("{0}, {1}", s.ResearchgroupName, s.Institutions.InstitutionName)
               });

            ViewBag.ResearchgroupList = new SelectList(ResearchgroupList.AsEnumerable(), "ResearchgroupID", "Researchgroup");

            ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Researchgroup Role in MultiPARTProject" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            var project = db.MultiPARTProjects.Find(projectid);

            var researchGroupInProjectVm = new ResearchgroupInMultiPARTProjectViewModel() { MultiPARTProjectID = projectid, ProjectName = project.MultiPARTProjectName };

            return View(researchGroupInProjectVm);
        }

        [HttpPost]
        public ActionResult AddResearchgroupModal(ResearchgroupInMultiPARTProjectViewModel researchgroupinprojectVM)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(422);

            if (researchgroupinprojectVM == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(researchgroupinprojectVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var researchgroupinproject = new ResearchgroupInMultiPARTProject()
            {
                MultiPARTProjectMultiPARTProjectID = researchgroupinprojectVM.MultiPARTProjectID,
                ResearchgroupResearchgroupID = researchgroupinprojectVM.ResearchgroupID,
                ResearchgroupRoleinMultiPARTProjectID = researchgroupinprojectVM.ResearchgroupRoleinMultiPARTProjectID,
                RegistrationDate = (DateTime)researchgroupinprojectVM.RegistrationDate,
                CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                CreatedOn = DateTimeOffset.Now,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };

            db.ResearchgroupInMultiPARTProjects.Add(researchgroupinproject);
            db.SaveChanges();
            return PartialView("_ResearchGroup", GetResearchGroupsInProject(researchgroupinproject.MultiPARTProjectMultiPARTProjectID));
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult AddResearchgroupModal(int projectid = 0)
        {
            var ResearchgroupList = db.Researchgroups
               .Where(r => r.Status == "Current"&& r.ResearchgroupInMultiPARTProjects.Where(rip => rip.Status == "Current" && rip.MultiPARTProjectMultiPARTProjectID == projectid).Count() == 0)
               .ToList()
               .OrderBy(c => c.ResearchgroupName)
               .Select(s => new
               {
                   ResearchgroupID = s.ResearchgroupID,
                   Researchgroup = string.Format("{0}, {1}", s.ResearchgroupName, s.Institutions.InstitutionName)
               });

            ViewBag.ResearchgroupList = new SelectList(ResearchgroupList.AsEnumerable(), "ResearchgroupID", "Researchgroup");
            ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Researchgroup Role in MultiPARTProject" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");
            var project = db.MultiPARTProjects.Find(projectid);
            var researchGroupInProjectVm = new ResearchgroupInMultiPARTProjectViewModel() { MultiPARTProjectID = projectid, ProjectName = project.MultiPARTProjectName };

            return PartialView("_AddResearchgroupToProject ", researchGroupInProjectVm);
        }

        //
        // POST: /MultiPARTProject/AddResearchgroupToProject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResearchgroupToProject(ResearchgroupInMultiPARTProjectViewModel researchgroupinprojectVM)
        {
            if (!ModelState.IsValid)
            {
                var researchGroupList = db.Researchgroups
                    .Where(x => x.Status == "Current")
                    .ToList()
                    .OrderBy(c => c.ResearchgroupName)
                    .Select(s => new
                    {
                          ResearchgroupID = s.ResearchgroupID,
                          Researchgroup = string.Format("{0}, {1}", s.ResearchgroupName, s.Institutions.InstitutionName)
                      });
                ViewBag.ResearchgroupList = new SelectList(researchGroupList.AsEnumerable(), "ResearchgroupID", "Researchgroup");
                ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Researchgroup Role in MultiPARTProject" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

                return View(researchgroupinprojectVM);
            }

            if (researchgroupinprojectVM == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(researchgroupinprojectVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var researchgroupinproject = new ResearchgroupInMultiPARTProject()
            {
                MultiPARTProjectMultiPARTProjectID = researchgroupinprojectVM.MultiPARTProjectID,
                ResearchgroupResearchgroupID = researchgroupinprojectVM.ResearchgroupID,
                ResearchgroupRoleinMultiPARTProjectID = researchgroupinprojectVM.ResearchgroupRoleinMultiPARTProjectID,
                RegistrationDate = (DateTime)researchgroupinprojectVM.RegistrationDate,

                CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                CreatedOn = DateTimeOffset.Now,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };

            db.ResearchgroupInMultiPARTProjects.Add(researchgroupinproject);
            db.SaveChanges();
            return RedirectToAction("Details", new { projectid = researchgroupinprojectVM.MultiPARTProjectID });
        }

        //
        // GET: /MultiPARTProject/EditResearchgroupToProject/
        public ActionResult EditResearchgroupToProject(int researchgroupinprojectid = 0)
        {
            ResearchgroupInMultiPARTProject researchgroupinproject = db.ResearchgroupInMultiPARTProjects.Find(researchgroupinprojectid);

            if (researchgroupinproject == null) return HttpNotFound();

            if (_projectuservalidationservice.ResearchgroupAssignedCohort(researchgroupinproject.ResearchgroupResearchgroupID, researchgroupinproject.MultiPARTProjectMultiPARTProjectID) && researchgroupinproject.Options.OptionValue == "Wet Lab")
            {
                ViewBag.ErrorMessage = "Research group has been cohort assigned. If you want to change the research group's role to Dry Lab, you will need to delete all cohort research group assignments.";
                return View("Error");
            }

            if (!_projectuservalidationservice.UserCanEditProject(researchgroupinproject.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }



            var researchGroupInProjectVm = new ResearchgroupInMultiPARTProjectViewModel()
            {
                ResearchgroupInMultiPARTProjectID = researchgroupinproject.ResearchgroupInMultiPARTProjectID,
                MultiPARTProjectID = researchgroupinproject.MultiPARTProjectMultiPARTProjectID,
                ResearchgroupID = researchgroupinproject.ResearchgroupResearchgroupID,
                ResearchgroupRoleinMultiPARTProjectID = researchgroupinproject.ResearchgroupRoleinMultiPARTProjectID,
                RegistrationDate = researchgroupinproject.RegistrationDate,
                ResearchgroupName = researchgroupinproject.Researchgroups.ResearchgroupName,
                ResearchgroupRoleinMultiPARTProject = researchgroupinproject.Options.OptionValue,
            };

            //var researchGroupList = db.Researchgroups
            //                        .Where(x => x.Status == "Current")
            //                        .ToList()
            //                        .OrderBy(c => c.ResearchgroupName)
            //                        .Select(s => new
            //                        {
            //                            ResearchgroupID = s.ResearchgroupID,
            //                            Researchgroup = string.Format("{0}, {1}", s.ResearchgroupName, s.Institutions.InstitutionName)
            //                        });
    //        ViewBag.ResearchgroupList = new SelectList(researchGroupList.AsEnumerable(), "ResearchgroupID", "Researchgroup");
            ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Researchgroup Role in MultiPARTProject" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            return View(researchGroupInProjectVm);
        }

        //
        // POST: /MultiPARTProject/EditResearchgroupToProject/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditResearchgroupToProject(ResearchgroupInMultiPARTProjectViewModel researchgroupinprojectVM)
        {
            if (!ModelState.IsValid)  
            {
                ViewBag.ResearchgroupRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "Researchgroup Role in MultiPARTProject" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");
                return View(researchgroupinprojectVM);
            }
            if (researchgroupinprojectVM == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(researchgroupinprojectVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            if (_projectuservalidationservice.ResearchgroupAssignedCohort(researchgroupinprojectVM.ResearchgroupID, researchgroupinprojectVM.MultiPARTProjectID) && researchgroupinprojectVM.ResearchgroupRoleinMultiPARTProject == "Dry Lab")
            {
                ViewBag.ErrorMessage = "Research group has been cohort assigned. If you want to change the research group's role to Dry Lab, you will need to delete all cohort research group assignments.";
                return View("Error");
            }

            ResearchgroupInMultiPARTProject researchgroupinproject = db.ResearchgroupInMultiPARTProjects.Find(researchgroupinprojectVM.ResearchgroupInMultiPARTProjectID);

            if (researchgroupinproject.Status != "Current")
            {
                ViewBag.ErrorMessage = "The instance has already been deleted. ";
                return View("Error");
            }

     //       researchgroupinproject.ResearchgroupInMultiPARTProjectID = researchgroupinprojectVM.ResearchgroupInMultiPARTProjectID;
     //       researchgroupinproject.ResearchgroupResearchgroupID = researchgroupinprojectVM.ResearchgroupID;
       //     researchgroupinproject.MultiPARTProjectMultiPARTProjectID = researchgroupinprojectVM.MultiPARTProjectID;
            researchgroupinproject.ResearchgroupRoleinMultiPARTProjectID = researchgroupinprojectVM.ResearchgroupRoleinMultiPARTProjectID;
       //     researchgroupinproject.RegistrationDate = (DateTime)researchgroupinprojectVM.RegistrationDate;

            researchgroupinproject.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.Entry(researchgroupinproject).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { projectid = researchgroupinprojectVM.MultiPARTProjectID });
        }

        [HttpDelete]
        public ActionResult RemoveResearchGroupFromProject(int researchgroupinprojectid)
        {
            ResearchgroupInMultiPARTProject researchgroupinproject = db.ResearchgroupInMultiPARTProjects.Find(researchgroupinprojectid);

            if (researchgroupinproject == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(researchgroupinproject.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            researchgroupinproject.Status = "Deleted";
            db.SaveChanges();
            return PartialView("_ResearchGroup", GetResearchGroupsInProject(researchgroupinproject.MultiPARTProjectMultiPARTProjectID));
            //return RedirectToAction("Details", new { projectid = researchgroupinproject.MultiPARTProjectMultiPARTProjectID });
        }

        //
        // GET: /MultiPARTProject/AddResearchgroupToProject
        [HttpGet]
        public ActionResult AddUserProjectAssignment(int researchgroupinprojectid = 0)
        {
            ResearchgroupInMultiPARTProject researchgroupinmultipartproject = db.ResearchgroupInMultiPARTProjects.Find(researchgroupinprojectid);

            if (researchgroupinmultipartproject == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(researchgroupinmultipartproject.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var userlist = db.UserInResearchgroups
               .Where(x => x.Status == "Current" && (x.EndTime == null || x.EndTime > DateTime.Now) && x.UserProfiles.Status == "Current" && x.Researchgroups.Status == "Current"
                   && x.ResearchgroupResearchgroupID == researchgroupinmultipartproject.ResearchgroupResearchgroupID)
               .ToList()
               .OrderBy(c => c.UserProfiles.SurName)
               .Select(s => new
               {
                   id = s.UserInResearchgroupID,
                   name = string.Format("{0} {1}", s.UserProfiles.ForeName, s.UserProfiles.SurName)
               });

            ViewBag.userlist = new SelectList(userlist.AsEnumerable(), "id", "name");
            ViewBag.UserRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "User Role in Project" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

            ViewBag.MultiPARTProjectID = researchgroupinmultipartproject.MultiPARTProjectMultiPARTProjectID;

            var userProjectAssignmentsVM = new UserProjectAssignmentViewModel()
            {
                ProjectName = researchgroupinmultipartproject.MultiPartProject.MultiPARTProjectName,
                ResearchgroupInMultiPARTProjectID = researchgroupinprojectid,
                MultiPARTProjectID = researchgroupinmultipartproject.MultiPARTProjectMultiPARTProjectID,
                ResearchgroupID = researchgroupinmultipartproject.ResearchgroupResearchgroupID,
            };

            return View(userProjectAssignmentsVM);
        }

        //
        // POST: /MultiPARTProject/AddUserProjectAssignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserProjectAssignment(UserProjectAssignmentViewModel userprojectassignmentVM)
        {
            if (!ModelState.IsValid)
            {
                var userlist = db.UserInResearchgroups
                                  .Where(x => x.Status == "Current" && (x.EndTime == null || x.EndTime > DateTime.Now) && x.UserProfiles.Status == "Current" && x.Researchgroups.Status == "Current"
                                      && x.ResearchgroupResearchgroupID == userprojectassignmentVM.ResearchgroupID)
                                  .ToList()
                                  .OrderBy(c => c.UserProfiles.SurName)
                                  .Select(s => new
                                  {
                                      id = s.UserInResearchgroupID,
                                      name = string.Format("{0} {1}", s.UserProfiles.ForeName, s.UserProfiles.SurName)
                                  });

                ViewBag.userlist = new SelectList(userlist.AsEnumerable(), "id", "name");
                ViewBag.UserRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "UserRoleinProject" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");

                ViewBag.projectid = userprojectassignmentVM.MultiPARTProjectID;

                return View(userprojectassignmentVM);
            }

            if (userprojectassignmentVM == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(userprojectassignmentVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var userprojectassignment = new UserProjectAssignment()
            {
                UserInResearchgroupUserInResearchgroupID = userprojectassignmentVM.UserInResearchgroupID,
                ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID = userprojectassignmentVM.ResearchgroupInMultiPARTProjectID,
                UserRoleinProjectID = userprojectassignmentVM.UserRoleinProjectID,

                CreatedBy = (int)Membership.GetUser().ProviderUserKey,
                CreatedOn = DateTimeOffset.Now,
                LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey
            };

            db.UserProjectAssignments.Add(userprojectassignment);
            db.SaveChanges();
            return RedirectToAction("Details", new { projectid = userprojectassignmentVM.MultiPARTProjectID });

        }

        //
        // GET: /MultiPARTProject/EditResearchgroupToProject?userprojectassignmentid=1
        public ActionResult EditUserProjectAssignment(int userprojectassignmentid = 0)
        {
            UserProjectAssignment userprojectassignment = db.UserProjectAssignments.Find(userprojectassignmentid);

            if (userprojectassignment == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(userprojectassignment.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            var userProjectAssignmentsVm = new UserProjectAssignmentViewModel()
            {
                UserProjectAssignmentID = userprojectassignment.UserProjectAssignmentID,
                UserRoleinProjectID = userprojectassignment.UserRoleinProjectID,
                MultiPARTProjectID = userprojectassignment.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID,
            };

            ViewBag.UserRoleList = new SelectList(db.Options.Where(o => o.OptionFields.OptionFieldName == "User Role in Project" && o.Status == "Current").AsEnumerable(), "OptionID", "OptionValue");
            ViewBag.MultiPARTProjectID = userProjectAssignmentsVm.MultiPARTProjectID;

            return View(userProjectAssignmentsVm);
        }

        //
        // POST: /MultiPARTProject/EditResearchgroupToProject?userprojectassignmentid=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProjectAssignment(UserProjectAssignmentViewModel userprojectassignmentsVM)
        {
            if (!ModelState.IsValid) return View(userprojectassignmentsVM);

            if (userprojectassignmentsVM == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(userprojectassignmentsVM.MultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            UserProjectAssignment userprojectassignments = db.UserProjectAssignments.Find(userprojectassignmentsVM.UserProjectAssignmentID);

            userprojectassignments.UserRoleinProjectID = userprojectassignmentsVM.UserRoleinProjectID;

            userprojectassignments.LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            db.Entry(userprojectassignments).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { projectid = userprojectassignmentsVM.MultiPARTProjectID });

        }

        //
        // POST: /MultiPARTProject/DeleteUserProjectAssignment?userprojectassignmentid=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserProjectAssignment(int userprojectassignmentid)
        {
            UserProjectAssignment userprojectassignments = db.UserProjectAssignments.Find(userprojectassignmentid);

            if (userprojectassignments == null) return HttpNotFound();

            if (!_projectuservalidationservice.UserCanEditProject(userprojectassignments.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID))
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            userprojectassignments.Status = "Deleted";
            db.SaveChanges();
            return RedirectToAction("Details", new { projectid = userprojectassignments.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID });
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}