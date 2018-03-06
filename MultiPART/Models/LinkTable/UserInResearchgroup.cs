using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.LinkTable
{
    public class UserInResearchgroup : ISoftDeletable
    {

        public UserInResearchgroup()
        {
            UserProjectAssignments = new HashSet<UserProjectAssignment>();
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

            Status = "Current";
        }

        [Key]
        public int UserInResearchgroupID { get; set; }

        public int UserProfileUserId { get; set; }

        public int ResearchgroupResearchgroupID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        [ForeignKey("Options")]
        public int UserRoleinResearchgroupID { get; set; }

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
        public virtual UserProfile UserProfiles { get; set; }
        public virtual Option Options { get; set; }
        public virtual Researchgroup Researchgroups { get; set; }


        public virtual ICollection<UserProjectAssignment> UserProjectAssignments { get; set; }
    }
}