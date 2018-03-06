using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class DataEntryDesign : ISoftDeletable
    {
        public DataEntryDesign()
        {
            Status = "Current";
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;

            DataEntries = new HashSet<DataEntry>();

        }

        public int DataEntryDesignID { get; set; }

        [ForeignKey("Procedure")]
        public int ProcedureProcedureID { get; set; }

        [ForeignKey("DataEntryField")]
        public int DataEntryFieldDataEntryFieldID { get; set; }

        public bool Mandatory { get; set; }

        public bool Multiple { get; set; }

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
        public bool Active
        {
            get { return (Status != "Deleted"); }
        }


        //virtual

        public virtual Procedure Procedure { get; set; }

        public virtual ICollection<DataEntry> DataEntries { get; set; }

        public virtual DataEntryField DataEntryField { get; set; }

        


        }
}