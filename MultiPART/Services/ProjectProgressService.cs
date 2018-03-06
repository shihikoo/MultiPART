using MultiPART.Models;
using MultiPART.Models.LinkTable;
using MultiPART.Models.Table;
using MultiPART.Repositories;
using MultiPART.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiPART.Services
{
    public interface IProjectProgressService
    {
        int GetAnimalAssignmentNumber(int ProjectID, int ResearchgroupID);
        int GetAnimalCreationNumber(int ProjectID, int ResearchgroupID);
        int GetAnimalDeathNumber(int ProjectID, int ResearchgroupID);
        int GetAnimalCompleteNumber(int projectid, int researchgroupid);

       bool ProjectIsValid(int ProjectID);
       bool ProcedureIsValid(int procedureid);
       bool ReserachgroupIsValid(int ResearchgroupID);
        bool HasCohortAssigned(int projectid, int researchgroupid);
        IEnumerable<Cohort> GetCohortsForResearchGroupAndProject(int ProjectID, int ResearchgroupID);       
        Procedure FindDeathReport(int ProjectID);
        IEnumerable<Procedure> FindOutcome(int ProjectID, int AnimalID);
        bool AnimalIsDead(int AnimalID);
        bool ProcedureCompleted(int ProcedureID, int AnimalID);
        bool OutcomeCompleted(int projectid, int animalid);
    }

    public class ProjectProgressService : IProjectProgressService
    {
        private IValidationDictionary _validatonDictionary; 
        private readonly int _userKey;
        private readonly string[] _roles;
        private readonly IGenericRepository<MultiPARTProject, int> _project;
        private readonly IGenericRepository<Researchgroup, int> _reserachgroup;
        private readonly IGenericRepository<Procedure, int> _procedure;
        private readonly IGenericRepository<Cohort, int> _cohort;
        private readonly IGenericRepository<Animal, int> _animal;
        private readonly IGenericRepository<ResearchgroupCohortAssignment, int> _ResearchgroupCohortAssignments;
        private readonly IGenericRepository<CohortProcedureAssignment, int> _CohortProcedureAssignment;
        private readonly IGenericRepository<Administration, int> _administration;
        private readonly IGenericRepository<AnimalAdministration, int> _animalAdministration;
        private readonly IGenericRepository<ResearchgroupInMultiPARTProject, int> _researchgroupInMultiPARTProjects;

        public ProjectProgressService(IValidationDictionary validationDictionary, IUnitOfWork uow, string[] roles)
            : this(validationDictionary, uow, 0, roles)
        {
        }

        public ProjectProgressService(IValidationDictionary validationDictionary, IUnitOfWork uow, int userKey, string[] roles)
        {
             _validatonDictionary = validationDictionary;
             _userKey = userKey;
             _roles = roles;
             _animal = uow.GetRepository<Animal>();
             _cohort = uow.GetRepository<Cohort>();
             _procedure = uow.GetRepository<Procedure>();
             _reserachgroup = uow.GetRepository<Researchgroup>();
             _project = uow.GetRepository<MultiPARTProject>();
             _ResearchgroupCohortAssignments = uow.GetRepository<ResearchgroupCohortAssignment>();
             _CohortProcedureAssignment = uow.GetRepository<CohortProcedureAssignment>(); 
            _researchgroupInMultiPARTProjects = uow.GetRepository<ResearchgroupInMultiPARTProject>();
            _animalAdministration = uow.GetRepository<AnimalAdministration>();
            _administration = uow.GetRepository<Administration>();             
        }

        public int GetAnimalAssignmentNumber(int ProjectID, int ResearchgroupID)
        {
            if (!(ProjectIsValid(ProjectID) && ReserachgroupIsValid(ResearchgroupID))) return 0;  

            return _ResearchgroupCohortAssignments.GetCurrent()
                .Where(rca => rca.ResearchgroupID == ResearchgroupID && rca.Cohorts.MultiPARTProjectMultiPARTProjectID == ProjectID && rca.Cohorts.Status == "Current")
                .Sum(rca => rca.NumberOfAnimals);
        }

        public int GetAnimalCreationNumber(int ProjectID, int ResearchgroupID)
        {
            if (!(ProjectIsValid(ProjectID) && ReserachgroupIsValid(ResearchgroupID))) return 0;

            var cohorts = GetCohortsForResearchGroupAndProject(ProjectID, ResearchgroupID);

            return _animal.GetCurrent().Where(a=>a.ResearchgroupID == ResearchgroupID && cohorts.Contains(a.Cohort)).Count();
        }

        public int GetAnimalDeathNumber(int ProjectID, int ResearchgroupID)
        {
            if (!(ProjectIsValid(ProjectID) && ReserachgroupIsValid(ResearchgroupID))) return 0;

            Procedure DeathReport = FindDeathReport(ProjectID);
            if (DeathReport == null) return 0;
            if (DeathReport.Administrations.Count() == 0) return 0;
            IEnumerable<Cohort> cohorts = GetCohortsForResearchGroupAndProject(ProjectID, ResearchgroupID);

            var foo = (GetAnimalAdministration(1,1) == null);

            var animals = _animal.GetCurrent().AsEnumerable().Where(a => cohorts.Contains(a.Cohort)
                && a.ResearchgroupID == ResearchgroupID
                && a.Cohort.CohortProcedureAssignments
                       .Where(cpa => cpa.Status == "Current" && cpa.ProcedureID == DeathReport.ProcedureID &&
                        (GetAnimalAdministration(a.AnimalID, cpa.Procedures.Administrations.FirstOrDefault().AdministrationID) != null)
                           ).Count() > 0);

            return animals.Count();
        }

        public int GetAnimalCompleteNumber(int projectid, int researchgroupid)
        {
            if (!(ProjectIsValid(projectid) && ReserachgroupIsValid(researchgroupid))) return 0;

            IEnumerable<Cohort> cohorts = GetCohortsForResearchGroupAndProject(projectid, researchgroupid);

            var animals = _animal.GetCurrent().AsEnumerable().Where(a => cohorts.Contains(a.Cohort) && a.ResearchgroupID == researchgroupid &&
                 OutcomeCompleted(projectid, a.AnimalID) && !AnimalIsDead(a.AnimalID));

            return animals.Count();
        }

        public bool OutcomeCompleted(int projectid, int animalid)
        {
            IEnumerable<Procedure> outcomes = FindOutcome(projectid, animalid);

            if (outcomes == null) return false;
            foreach (var outcome in outcomes)
            {
                if (!ProcedureCompleted(outcome.ProcedureID, animalid)) return false;

            }
            return true;
        }

        public bool ProcedureCompleted(int ProcedureID, int AnimalID)
        {
            var AdministrationIDs = _administration.GetCurrent().Where(a => a.ProcedureID == ProcedureID && a.Procedure.Status == "Current").Select(a => a.AdministrationID);

            if (AdministrationIDs.Count() == 0) return false;

            foreach (var AdministrationID in AdministrationIDs)
            { if (GetAnimalAdministration(AnimalID, AdministrationID) == null) return false; }

            return true;
        }

        public bool AnimalIsDead(int AnimalID)
        {
            var animal = _animal.GetCurrent().Where(a => a.AnimalID == AnimalID).FirstOrDefault();

            if (animal == null) return false;

            Procedure DeathReport = FindDeathReport(animal.Cohort.MultiPARTProjectMultiPARTProjectID);

            if (DeathReport == null) return false;

            var AdministrationID = _administration.GetCurrent().Where(a => a.Status == "Current" && a.ProcedureID == DeathReport.ProcedureID).FirstOrDefault().AdministrationID;
            if (GetAnimalAdministration(AnimalID, AdministrationID) != null) return true;

            return false;
        }

        public AnimalAdministration GetAnimalAdministration(int AnimalID, int AdministrationID)
        {
            try
            {
                return
    _animalAdministration.GetCurrent().FirstOrDefault(ap => ap.AnimalID == AnimalID && ap.AdministrationID == AdministrationID);

            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<Procedure> FindOutcome(int ProjectID, int AnimalID)
        {
            return _CohortProcedureAssignment.GetCurrent().Where(cpa => cpa.Cohorts.Status == "Current" && cpa.Procedures.Status == "Current"
                && cpa.Procedures.MultiPARTProjectMultiPARTProjectID == ProjectID
                && cpa.Procedures.OptionsProcedurePurpose.OptionValue == "Outcome Assessment"
                && cpa.Cohorts.Animals.Select(a => a.AnimalID).Contains(AnimalID)
                )
                .Select(cpa => cpa.Procedures);
        }

        public Procedure FindDeathReport(int ProjectID)
        {
            return _procedure.GetCurrent().Where(p => p.MultiPARTProjectMultiPARTProjectID == ProjectID && p.OptionsProcedurePurpose.OptionValue == "Mortality Report").FirstOrDefault();
        }

        public IEnumerable<Researchgroup> GetResearchgroupsForProject(int projectid)
        {
            return _researchgroupInMultiPARTProjects.GetCurrent().Where(rip => rip.MultiPARTProjectMultiPARTProjectID == projectid && rip.Researchgroups.Status == "Current").Select(rip => rip.Researchgroups);
        }

        public IEnumerable<Cohort> GetCohortsForResearchGroupAndProject(int ProjectID, int ResearchgroupID)
        {
            return _cohort.GetCurrent()
                .Where(c => c.MultiPARTProjectMultiPARTProjectID == ProjectID 
                    && c.ResearchgroupCohortAssignments.Where(rcap => rcap.Status == "Current").Select(rca => rca.ResearchgroupID).Contains(ResearchgroupID));
        }

        public bool HasCohortAssigned(int ProjectID, int ResearchgroupID)
        {
            return GetCohortsForResearchGroupAndProject(ProjectID, ResearchgroupID).Count() > 0;
        }

        public bool ReserachgroupIsValid(int ResearchgroupID)
        {
            var reserachgroupNumber = _reserachgroup.GetCurrent().Where(p => p.ResearchgroupID == ResearchgroupID).Count();
            if (reserachgroupNumber == 0) return false;

            return true;
        }

        public bool ProjectIsValid(int ProjectID)
        {
            var projectNumber =_project.GetCurrent().Where(p=> p.MultiPARTProjectID == ProjectID).Count();
            if (projectNumber == 0) return false;

            return true;
        }

        public bool ProcedureIsValid(int procedureid)
        {
            var procedure = _procedure.GetCurrent().Where(p=>p.ProcedureID == procedureid);
            if (procedure == null) return false;

            return true;
        }
    }
}