using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using MultiPART.Models.Table;

namespace MultiPART.Models.LinkTable
{
    public class Careerhistory : ISoftDeletable
{
        public Careerhistory()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

        }

        [Key]
        public int CareerhistoryID { get; set; }

        [ForeignKey("UserProfile")]
        public int UserProfileUserId { get; set; }

         [ForeignKey("Institutions")]
        public int InstitutionInstitutionID { get; set; }
    
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Position { get; set; }

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
        public virtual UserProfile UserProfile { get; set; }

        public virtual Institution Institutions { get; set; }

    }
}