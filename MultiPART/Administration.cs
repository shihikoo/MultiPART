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
    
    public partial class Administration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administration()
        {
            this.AnimalAdministrations = new HashSet<AnimalAdministration>();
        }
    
        public int AdministrationID { get; set; }
        public int ProcedureID { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
    
        public virtual Procedure Procedure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalAdministration> AnimalAdministrations { get; set; }
    }
}
