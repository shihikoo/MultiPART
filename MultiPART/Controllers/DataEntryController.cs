using System.Collections.Generic;
using System.Web.Routing;
using System.Web.Security;
using MultiPART.Models;
using System;
using System.Linq;
using MultiPART.Models.LinkTable;
using MultiPART.Models.ViewModel;
using MultiPART.Models.Table;
using System.Web.Mvc;
using MultiPART.Models.ViewModel.DataEntryViewModels;
using MultiPART.Services;
using MultiPART.UnitOfWork;
using MultiPART.Authorization;

namespace MultiPART.Controllers
{
    [Restrict("Disabled")]
    [Authorize(Roles = "Administrator, superuser, poweruser,user")]
    public class DataEntryController : Controller
    {
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly DataEntryService _dataEntryService;
        private readonly ProjectUserValidationService _projectuservalidationservice;


        public DataEntryController()
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
        }

        //
        // GET: /DataEntry/

        public ActionResult DataEntryDesign(int procedureid, int projectid)
        {
            var access = _projectuservalidationservice.GetUserProjectAccessType(projectid);

            if (access == AssessType.None)
            {
                ViewBag.ErrorMessage = "Access Denied. Please contact administrator for further assistance.";
                return View("Error");
            }

            if (access == AssessType.Edit)
            {
                var dataEntryDesignVms = _dataEntryService.GetDesignViewModels(procedureid);

                ViewBag.projectid = projectid;
                ViewBag.procedureid = procedureid;
                return View(dataEntryDesignVms);
            }
            if (access == AssessType.View)
            {
                var formPreview = _dataEntryService.GetFormPreviewModel(procedureid);

                return View("FormPreview", formPreview);
            }

            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataEntryDesign(IEnumerable<DataEntryDesignViewModel> dataentrydesignVm, int projectid, int procedureid)
        {
            var dataEntryDesigns = _dataEntryService.GetDataEntryDesigns(dataentrydesignVm);

            if (ModelState.IsValid)
            {
                _dataEntryService.ValidateDesigns(dataEntryDesigns);
                _uow.Save();
            }

            ViewBag.projectid = projectid;

            return RedirectToAction("DataEntryDesign", new { projectid = projectid, procedureid = procedureid });
        }

        public ActionResult FormPreview(int procedureid)
        {
            var formPreview = _dataEntryService.GetFormPreviewModel(procedureid);
            return View(formPreview);
        }

        [HttpGet]
        public ActionResult DataEntry(int administrationId, int procedureId, int animalId, int projectId, int researchGroupId, int diseaseModelInductionId)
        {
            //Check if animal exists
            var animal = _uow.GetRepository<Animal>().Get(animalId);
            if (animal == null)
            {
                ViewBag.ErrorMessage = "No animal was found.";
                return View("Error");
            }

            //Check procedure is assigned to animals cohort
            int cohortProcedureAssignmentNumber = _uow.GetRepository<CohortProcedureAssignment>()
                .GetCurrent().Count(cpa => cpa.Cohorts.CohortID == animal.CohortID && cpa.ProcedureID == procedureId);

            if (cohortProcedureAssignmentNumber != 1)
            {
                ViewBag.ErrorMessage = "This proceudre is not yet assigned to this cohort.";
                return View("Error");
            }

            //Get collection of DataEntryView Models
            var dataEntryVms = _dataEntryService.GetDataEntryViewModels(administrationId, animalId, projectId, researchGroupId, diseaseModelInductionId);

            return View(dataEntryVms);
        }

        public ActionResult GetFile(int FileID)
        {
            var file = _uow.GetRepository<File>().Get(FileID);
            return File(file.FileUrl, file.FileType);
        }

        public ActionResult DataEntryDisplay(int administrationId, int procedureId, int animalId, int projectId, int researchGroupId, int diseaseModelInductionId)
        {
            //Check if animal exists
            var animal = _uow.GetRepository<Animal>().Get(animalId);
            if (animal == null)
            {
                ViewBag.ErrorMessage = "No animal was found.";
                return View("Error");
            }

            //Check procedure is assigned to animals cohort
            int cohortProcedureAssignmentNumber = _uow.GetRepository<CohortProcedureAssignment>()
                .GetCurrent().Count(cpa => cpa.Cohorts.CohortID == animal.CohortID && cpa.ProcedureID == procedureId);

            if (cohortProcedureAssignmentNumber != 1)
            {
                ViewBag.ErrorMessage = "This proceudre is not yet assigned to this cohort.";
                return View("Error");
            }

            //Get collection of DataEntryView Models
            var dataEntryVms = _dataEntryService.GetDataEntryViewModels(administrationId, animalId, projectId, researchGroupId, diseaseModelInductionId);

            return View(dataEntryVms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataEntry(DataEntryViewModel dataEntryViewModel)
        {
            //Convert dataEntryViewModels into relevant entities for persistance
            if (!_dataEntryService.CreateExperimentData(dataEntryViewModel))
            {
                _dataEntryService.RepopulateViewModel(dataEntryViewModel);
                return View(dataEntryViewModel);
            }

            _uow.Save();

            return RedirectToAction("DataEntryDisplay", new
            {
                procedureId = dataEntryViewModel.ProcedureID,
                administrationId = dataEntryViewModel.AdministrationID,
                animalId = dataEntryViewModel.AnimalID,
                projectId = dataEntryViewModel.ProjectID,
                researchGroupId = dataEntryViewModel.ResearchGroupID,
                diseaseModelInductionId = dataEntryViewModel.DiseaseModelInductionID
            }
                );
        }




    }
}
