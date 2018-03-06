using MultiPART.Models.Table;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.LinkTable
{
    public class ResearchgroupCohortAssignment : ISoftDeletable
    {
        public ResearchgroupCohortAssignment()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

        }
           
        [Key]
        public int ResearchgroupCohortAssignmentID { get; set; }

        [ForeignKey("Cohorts")]
        public int CohortID { get; set; }

        [ForeignKey("Researchgroups")]
        public int ResearchgroupID { get; set; }

        public int NumberOfAnimals { get; set; }

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

        /*virtual*/
        public virtual Cohort Cohorts { get; set; }

        public virtual Researchgroup Researchgroups { get; set; }



    }
}