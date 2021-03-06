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
    
    public partial class DataEntry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataEntry()
        {
            this.DataEntryDetails = new HashSet<DataEntryDetail>();
            this.UserDataentryAssignments = new HashSet<UserDataentryAssignment>();
        }
    
        public int DataEntryID { get; set; }
        public int DataEntryDesignDataEntryDesignID { get; set; }
        public string Status { get; set; }
        public Nullable<int> AnimalAdministrationID { get; set; }
    
        public virtual AnimalAdministration AnimalAdministration { get; set; }
        public virtual DataEntryDesign DataEntryDesign { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataEntryDetail> DataEntryDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDataentryAssignment> UserDataentryAssignments { get; set; }
    }
}
