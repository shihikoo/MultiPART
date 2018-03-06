using MultiPART.Models.LookupTable;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.LinkTable
{
    public class UserProjectAssignment : ISoftDeletable
    {
        public UserProjectAssignment()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

        }

        [Key]
        public int UserProjectAssignmentID { get; set; }

        [ForeignKey("ResearchgroupInMultiPARTProjects")]
        public int ResearchgroupInMultiPARTProjectResearchgroupInMultiPARTProjectID { get; set; }

        [ForeignKey("UserInResearchgroups")]
        public int UserInResearchgroupUserInResearchgroupID { get; set; }

        [ForeignKey("Options")]
        public int UserRoleinProjectID { get; set; }

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

        //virtual
        public virtual ResearchgroupInMultiPARTProject ResearchgroupInMultiPARTProjects { get; set; }

        public virtual UserInResearchgroup UserInResearchgroups { get; set; }

        public virtual Option Options { get; set; }

    }
}