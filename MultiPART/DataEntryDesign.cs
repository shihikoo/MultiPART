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
    
    public partial class DataEntryDesign
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataEntryDesign()
        {
            this.DataEntries = new HashSet<DataEntry>();
        }
    
        public int DataEntryDesignID { get; set; }
        public int ProcedureProcedureID { get; set; }
        public int DataEntryFieldDataEntryFieldID { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
        public bool Mandatory { get; set; }
        public bool Multiple { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataEntry> DataEntries { get; set; }
        public virtual DataEntryField DataEntryField { get; set; }
        public virtual Procedure Procedure { get; set; }
    }
}
