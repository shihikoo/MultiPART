//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiPART
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcedureDetail
    {
        public int ProcedureDetailID { get; set; }
        public int ProcedureProcedureID { get; set; }
        public int ProcedureDetailOptionFieldID { get; set; }
        public Nullable<int> ProcedureDetailOptionID { get; set; }
        public string ProcedureDetailValue { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
    
        public virtual ProcedureDetailOptionField ProcedureDetailOptionField { get; set; }
        public virtual ProcedureDetailOption ProcedureDetailOption { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
