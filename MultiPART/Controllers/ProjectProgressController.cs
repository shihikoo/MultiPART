using MultiPART.Authorization;
using MultiPART.Models;
using MultiPART.Models.Table;
using MultiPART.Models.ViewModel;
using MultiPART.Services;
using MultiPART.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiPART.Controllers
{

    [Authorize(Roles = "Administrator, Superuser, Poweruser")]
    [Restrict("Disabled")]
    public class ProjectProgressController : Controller
    {
        private MultipartContext db = new MultipartContext();
        private readonly IUnitOfWork _uow = new UnitOfWork<MultipartContext>();
        private readonly DataEntryService _dataEntryService;
        private readonly ProjectProgressService _projectprogressService;
        //  private readonly UserAccessService _useraccessservice;

        public ProjectProgressController()
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null && membershipUser.ProviderUserKey != null)
            {
                string[] roles = Roles.GetRolesForUser(membershipUser.UserName);
                var userKey = (int)membershipUser.ProviderUserKey;
                _dataEntryService = new DataEntryService(new ModelStateWrapper(ModelState), _uow, userKey);
                _projectprogressService = new ProjectProgressService(new ModelStateWrapper(ModelState), _uow, userKey,roles);
            }
            else { throw new UnauthorizedAccessException("MembershipUser or ProviderUserKey is null - Is the user authenticated?"); }
        }

        //
        // GET: /Summary/

        public ActionResult Index(int projectid)
        {
            if (!_projectprogressService.ProjectIsValid(projectid))
            {
                ViewBag.ErrorMessage = "No project was found.";
                return View("Error");
            }

            MultiPARTProject project = _uow.GetRepository<MultiPARTProject>().Get(projectid);

            ProjectProgressListViewModel projectprogresslistVM = new ProjectProgressListViewModel()
            {
                ProjectID = projectid,
                ProjectName = project.MultiPARTProjectName,
                Progress = GetProjectProgress(projectid),
            };
            return View(projectprogresslistVM);
        }

        private IList<ProjectProgressViewModel> GetProjectProgress(int projectid)
        {
            var Researchgroups = _projectprogressService.GetResearchgroupsForProject(projectid);
    
            int userid = (int)Membership.GetUser().ProviderUserKey;

            if(!(User.IsInRole("Administrator") ||User.IsInRole("Superuser"))) 
            {
               Researchgroups = Researchgroups.Where(r=> r.UserInResearchgroups.Select(uir => uir.UserProfileUserId).Contains(userid));            
            }

            if (Researchgroups.Count() == 0) return null;

            var Progress = from r in Researchgroups
                           where _projectprogressService.ReserachgroupIsValid(r.ResearchgroupID) && _projectprogressService.HasCohortAssigned(projectid, r.ResearchgroupID)
                           select new ProjectProgressViewModel
                           {
                               ResearchgroupName = r.ResearchgroupName,
                               AssignedNumber = _projectprogressService.GetAnimalAssignmentNumber(projectid, r.ResearchgroupID),
                               CreatedNumber = _projectprogressService.GetAnimalCreationNumber(projectid, r.ResearchgroupID),
                               DeathNumber = _projectprogressService.GetAnimalDeathNumber(projectid, r.ResearchgroupID),
                               CompletedNumber = _projectprogressService.GetAnimalCompleteNumber(projectid, r.ResearchgroupID)
                           };

            return Progress.ToList();
        }

    }
}

