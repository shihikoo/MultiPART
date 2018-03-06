using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class BehavioralScore : ISoftDeletable
    {
        public BehavioralScore()
        {
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            LastUpdatedBy = (int)Membership.GetUser().ProviderUserKey;

            Status = "Current";
        }

        public int BehavioralScoreID { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }

        [ForeignKey("UserDataentryAssignment")]
        public int UserDataentryAssignmentID { get; set; }

        public int Score { get; set; }

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
        public virtual Question Question { get; set; }

        public virtual UserDataentryAssignment UserDataentryAssignment { get; set; }


    }
}