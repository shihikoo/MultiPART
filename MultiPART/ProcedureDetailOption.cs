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
    
    public partial class ProcedureDetailOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcedureDetailOption()
        {
            this.ProcedureDetails = new HashSet<ProcedureDetail>();
            this.Questions = new HashSet<Question>();
        }
    
        public int ProcedureDetailOptionID { get; set; }
        public int ProcedureDetailOptionFieldID { get; set; }
        public string ProcedureDetailOptionName { get; set; }
        public string Status { get; set; }
    
        public virtual ProcedureDetailOptionField ProcedureDetailOptionField { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcedureDetail> ProcedureDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}