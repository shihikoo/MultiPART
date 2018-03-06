using MultiPART.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultiPART.UnitOfWork;
using MultiPART.Repositories;
using MultiPART.Models.LinkTable;

namespace MultiPART.Services
{
    public interface IProjectUserValidationService
    {
        bool UserIsInProject(int ProjectID, string Role);
        IEnumerable<Researchgroup> ResearchgroupsOfUser();
        bool UserCanEditProject(int ProjectID);
        bool UserCanViewProject(int ProjectID);
        bool UserCanCreateAnimal (int ProjectID);
        bool UserCanEnterExperimentData (int ProjectID);
        bool ResearchgroupAssignedCohort(int ProjectID, int ResearchgroupID);

        MultiPART.AssessType GetUserProjectAccessType(int ProjectID);
    }

    public class ProjectUserValidationService : IProjectUserValidationService
    {
        private IValidationDictionary _validatonDictionary;
        private readonly int _userKey;
        private readonly string[] _roles;
        private readonly IGenericRepository<MultiPARTProject, int> _project;
        private readonly IGenericRepository<UserInResearchgroup, int> _userinreserachgroup;
        private readonly IGenericRepository<UserProjectAssignment, int> _userprojectassignment;
        private readonly IGenericRepository<ResearchgroupInMultiPARTProject, int> _researchgroupinmultipleproject;
        private readonly IGenericRepository<Researchgroup, int> _reserchgroup;
        private readonly IGenericRepository<ResearchgroupCohortAssignment, int> _researchgroupcohortassignment;

        public ProjectUserValidationService(IValidationDictionary validationDictionary, IUnitOfWork uow, string[] roles)
            : this(validationDictionary, uow, 0, roles)
        {
        }

        public ProjectUserValidationService(IValidationDictionary validationDictionary, IUnitOfWork uow, int userKey, string[] roles)
        {
             _validatonDictionary = validationDictionary;
             _userinreserachgroup = uow.GetRepository<UserInResearchgroup>();
             
             _userprojectassignment = uow.GetRepository<UserProjectAssignment>();
             _researchgroupinmultipleproject = uow.GetRepository<ResearchgroupInMultiPARTProject>();
             _project = uow.GetRepository<MultiPARTProject>();
             _reserchgroup = uow.GetRepository<Researchgroup>();
             _researchgroupcohortassignment = uow.GetRepository<ResearchgroupCohortAssignment>();
             _userKey = userKey;
             _roles = roles;
        }

        public bool ResearchgroupAssignedCohort(int ProjectID, int ResearchgroupID)
        { 
            return _researchgroupcohortassignment.GetCurrent()
                .Where(rca => rca.ResearchgroupID == ResearchgroupID && rca.Cohorts.MultiPARTProjectMultiPARTProjectID == ProjectID).Count() > 0; 
        }

        public bool UserCanCreateAnimal(int ProjectID)
        {
            if (UserIsInProject(ProjectID, "Drug Dispenser") || _roles.Contains("Administrator")) return true;

            return false;
        }

        public bool UserCanEnterExperimentData(int ProjectID)
        {
            if (UserIsInProject(ProjectID, "Experimenter") || _roles.Contains("Administrator")) return true;

            return false;
        }

        public MultiPART.AssessType GetUserProjectAccessType(int ProjectID)
        { 
            if(UserCanEditProject(ProjectID)) return AssessType.Edit;
            if(UserCanViewProject(ProjectID)) return AssessType.View;

            return AssessType.None;
        }

        public bool UserCanViewProject(int ProjectID)
        {
            if (UserIsInProject(ProjectID) || _roles.Contains("Superuser")) return true;

            return false;
        }

        public bool UserCanEditProject(int ProjectID)
        {
            if (UserIsInProject(ProjectID, "Principal Investigator") || _roles.Contains("Administrator")) return true;

            return false;       
        }

        public bool UserIsInProject(int ProjectID, string Role="")
        {
            if (!ProjectIsValid(ProjectID)) return false;

            return _userprojectassignment.GetCurrent().Where(u => 
                Role == "" ? true : (u.Options.OptionValue == Role.Trim()) 
                && u.ResearchgroupInMultiPARTProjects.MultiPARTProjectMultiPARTProjectID == ProjectID 
                && u.UserInResearchgroups.UserProfileUserId == _userKey
                && (u.UserInResearchgroups.EndTime == null || (u.UserInResearchgroups.EndTime > DateTime.Now) )).Count() > 0;
        }

        public bool ProjectIsValid(int ProjectID = 0)
        {

            if (ProjectID == 0) return false;
            if (_project.GetCurrent().Count(p => p.MultiPARTProjectID == ProjectID) == 1) return true;
            
            return false;
        }

        public IEnumerable<Researchgroup> ResearchgroupsOfUser()
        { 
            var query = from uir in _userinreserachgroup.GetCurrent().Where(u => u.UserProfileUserId == _userKey)
                        select uir.Researchgroups;

            return query;
        }

    }
}