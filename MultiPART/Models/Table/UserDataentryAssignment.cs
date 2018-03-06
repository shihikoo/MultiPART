using MultiPART.Models.LinkTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class UserDataentryAssignment : ISoftDeletable
    {
        public UserDataentryAssignment()
        {
            BehavioralScores = new HashSet<BehavioralScore>();

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            Status = "Current";
        }

        public int UserDataentryAssignmentID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        [ForeignKey("DataEntry")]
        public int DataEntryID { get; set; }

        //--------------------//
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

        //--------------------//
        /*virtual*/

        public virtual UserProfile Users { get; set; }

        public virtual DataEntry DataEntry { get; set; }

        public virtual ICollection<BehavioralScore> BehavioralScores { get; set; }
    }
}