using MultiPART.Models.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MultiPART.Models.LookupTable
{
    public class ProcedureDetailOptionField : ISoftDeletable
    {

        public ProcedureDetailOptionField()
        {
            Status = "Current";
            ProcedureDetailOptions = new HashSet<ProcedureDetailOption>();
            ProcedureDetails = new HashSet<ProcedureDetail>();
            Multiple = false;
        }

        public int ProcedureDetailOptionFieldID { get; set; }

        [ForeignKey("ProcedureDetailFieldType")]
        public int ProcedureDetailFieldTypeID { get; set; }

        [ForeignKey("ProcedurePurposeOrType")]
        public int ProcedurePurposeOrTypeID { get; set; }

        public string ProcedureDetailOptionFieldName { get; set; }

        // virtual
        public string Status { get; set; }

        public virtual bool Multiple { get; set; }

        public virtual Option ProcedurePurposeOrType { get; set; }

        public virtual Option ProcedureDetailFieldType { get; set; }

        public virtual ICollection<ProcedureDetailOption> ProcedureDetailOptions { get; set; }

        public virtual ICollection<ProcedureDetail> ProcedureDetails { get; set; }

    }
}