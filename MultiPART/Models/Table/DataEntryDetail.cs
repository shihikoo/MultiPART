using MultiPART.Models.LinkTable;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{

    public abstract class   DataEntryDetail : ISoftDeletable
    {
        public DataEntryDetail()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
        }

        [Key]
        public int DataEntryDetailID { get; set; }

        [ForeignKey("DataEntry")]
        public int DataEntryDataEntryID { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

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
        [NotMapped]
        public bool IsActive
        {
            get { return (Status != "Deleted"); }
        }
        
        /*virtual*/

        public virtual DataEntry DataEntry { get; set; }

         public void SoftDelete(bool cascade)
        {
            Status = "Deleted";
        }

    }
}