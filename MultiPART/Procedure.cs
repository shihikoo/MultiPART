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
    
    public partial class Procedure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Procedure()
        {
            this.Administrations = new HashSet<Administration>();
            this.DataEntryDesigns = new HashSet<DataEntryDesign>();
            this.ProcedureDetails = new HashSet<ProcedureDetail>();
        }
    
        public int ProcedureID { get; set; }
        public int MultiPARTProjectMultiPARTProjectID { get; set; }
        public Nullable<int> ProcedureTypeID { get; set; }
        public int ProcedurePurposeID { get; set; }
        public string ProcedureLabel { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administration> Administrations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataEntryDesign> DataEntryDesigns { get; set; }
        public virtual MultiPARTProject MultiPARTProject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcedureDetail> ProcedureDetails { get; set; }
    }
}
