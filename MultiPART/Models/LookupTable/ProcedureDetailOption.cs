using MultiPART.Models.Table;
using System.Collections.Generic;

namespace MultiPART.Models.LookupTable
{
    public class ProcedureDetailOption : ISoftDeletable
    {
        public ProcedureDetailOption()
        {
            ProcedureDetails = new HashSet<ProcedureDetail>();

            Status = "Current";
        }

        public int ProcedureDetailOptionID { get; set; }

        public int ProcedureDetailOptionFieldID { get; set; }

        public string ProcedureDetailOptionName { get; set; }

        public string Status { get; set; }


        public virtual ProcedureDetailOptionField ProcedureDetailOptionFields { get; set; }

        public virtual ICollection<ProcedureDetail> ProcedureDetails { get; set; }

    }
}