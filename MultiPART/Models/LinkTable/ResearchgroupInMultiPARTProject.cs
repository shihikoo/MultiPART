using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.LinkTable
{
    public class ResearchgroupInMultiPARTProject : ISoftDeletable
    {
        public ResearchgroupInMultiPARTProject()
        {
            UserProjectAssignments = new HashSet<UserProjectAssignment>();
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

            Status = "Current";
        }

        [Key]
        public int ResearchgroupInMultiPARTProjectID { get; set; }

        
        public int MultiPARTProjectMultiPARTProjectID { get; set; }

        public int ResearchgroupResearchgroupID { get; set; }

        public DateTime RegistrationDate { get; set; }

        [ForeignKey("Options")]
        public int ResearchgroupRoleinMultiPARTProjectID { get; set; }

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

        /*virtural*/
        public virtual MultiPARTProject MultiPartProject { get; set; }
        public virtual Researchgroup Researchgroups { get; set; }
        public virtual Option Options { get; set; }

        public virtual ICollection<UserProjectAssignment> UserProjectAssignments { get; set; }
    }
}