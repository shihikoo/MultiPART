using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class ProcedureDetail : ISoftDeletable
    {
        public ProcedureDetail()
        {
            Questions = new HashSet<Question>();

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            Status = "Current";
        }

        [Key]
        public int ProcedureDetailID { get; set; }

        [ForeignKey("Procedures")]
        public int ProcedureProcedureID { get; set; }

        [ForeignKey("ProcedureDetailOptionFields")]
        public int ProcedureDetailOptionFieldID { get; set; }

        [ForeignKey("ProcedureDetailOptions")]
        public int? ProcedureDetailOptionID { get; set; }

        public string ProcedureDetailValue { get; set; }

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
        public virtual Procedure Procedures { get; set; }
    
        public virtual ProcedureDetailOptionField ProcedureDetailOptionFields { get; set; }

        public virtual ProcedureDetailOption ProcedureDetailOptions { get; set; }
      
        //virtual collection
        public virtual ICollection<Question> Questions { get; set; }
    }
}