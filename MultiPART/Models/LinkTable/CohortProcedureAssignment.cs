using MultiPART.Models.Table;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.LinkTable
{
    public class CohortProcedureAssignment : ISoftDeletable
    {
        public CohortProcedureAssignment()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

        }

        [Key]
        public int CohortProcedureAssignmentID { get; set; }

        [ForeignKey("Cohorts")]
        public int CohortID { get; set; }

        [ForeignKey("Procedures")]
        public int ProcedureID { get; set; }

        //[ForeignKey("Options")]
        //public int RandomizationRole { get; set; }

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

        public virtual Procedure Procedures { get; set; }

    }
}