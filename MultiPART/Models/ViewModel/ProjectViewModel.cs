using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [Display(Name = "Project Name *")]
        public string ProjectName { get; set; }
        [Required]
        [Display(Name = "Objectives *")]
        public string Objectives { get; set; }

        [Display(Name = "Background")]
        public string Background { get; set; }

        [Display(Name = "Ethical Statement")]
        public string EthicalStatement { get; set; }

        [Display(Name = "Analysis Plan")]
        public string AnalysisPlan { get; set; }

        [Display(Name = "Funding")]
        public string Funding { get; set; }

        public string Logo { get; set; }

        [Display(Name = "Created by")]
        public string Creator { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Start Date *")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Expected Completion Date *")]
        public DateTime ExpectedCompletionDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Actual Completion Date")]
        public DateTime? CompletionDate { get; set; }
        
        [Display(Name = "Research Groups")]
        public IEnumerable<ResearchgroupInMultiPARTProjectViewModel> ResearchGroups { get; set; }

        [Display(Name = "Researchers")]
        public IEnumerable<UserProjectAssignmentListViewModel> Researchers { get; set; }

        public IEnumerable<ProcedureListViewModel> Comorbidities { get; set; }
        
        [Display(Name = "Disease Models")]
        public IEnumerable<ProcedureListViewModel> DiseaseModels { get; set; }

        public IEnumerable<ProcedureListViewModel> Treatments { get; set; }

        [Display(Name = "Outcome Methods")]
        public IEnumerable<ProcedureListViewModel> OutcomeAssessments { get; set; }

        [Display(Name = "Anesthesia")]
        public IEnumerable<ProcedureListViewModel> Anesthesia { get; set; }

        [Display(Name = "Post Op Analgesia")]
        public IEnumerable<ProcedureListViewModel> PostOpAnalgesia { get; set; }

        [Display(Name = "Mortality Report")]
        public IEnumerable<ProcedureListViewModel> MortalityReport { get; set; }

        public IEnumerable<CohortListViewModel> Cohorts { get; set; }
        
        [Display(Name = "Cohorts & Procedures")]
        public IEnumerable<CohortProcedureListViewModel> CohortProcedures { get; set; }

        [Display(Name = "Cohort & Research Groups")]
        public IEnumerable<CohortListViewModel> CohortResearchGroups { get; set; }
 
    }
}