using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MultiPART.Models.LinkTable;
using MultiPART.Models.Table;
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models
{
    /*Primary Animal Study Projects*/

    public class MultiPARTProject : ISoftDeletable
    {

        public MultiPARTProject()
        {
            ResearchgroupInMultiPARTProjects = new HashSet<ResearchgroupInMultiPARTProject>();
            Procedures = new HashSet<Procedure>();
            Cohorts = new HashSet<Cohort>();
            Status = "Current";

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

            AnimalHusbandries = new HashSet<AnimalHusbandry>();
        }

        [Key]
        public int MultiPARTProjectID { get; set; }

        public string MultiPARTProjectName { get; set; }

        public string Objectives { get; set; }

        public string Background { get; set; }

        [ForeignKey("Files")]
        public int? LogoID { get; set; }

        public string EthicalStatement { get; set; }

        public string AnalysisPlan { get; set; }

        public string Funding { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime ProjectCompletionDateExpected { get; set; }

        public DateTime? ProjectComletionDate { get; set; }

        public bool Completed { get; set; }

        public string Status { get; set; }

        public int CreatedBy { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public int LastUpdatedBy { get; set; }

        private DateTimeOffset? lastUpdatedOn;
        [Required]
        public DateTimeOffset LastUpdatedOn
        {
            get { return lastUpdatedOn ?? DateTimeOffset.Now; }
            set { lastUpdatedOn = value; }
        }

        public virtual File Files {get;set;}

        public virtual ICollection<ResearchgroupInMultiPARTProject> ResearchgroupInMultiPARTProjects { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; }

        public virtual ICollection<Cohort> Cohorts { get; set; }

        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }


    }
}