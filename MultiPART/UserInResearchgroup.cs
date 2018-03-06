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
    
    public partial class UserInResearchgroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserInResearchgroup()
        {
            this.UserProjectAssignments = new HashSet<UserProjectAssignment>();
        }
    
        public int UserInResearchgroupID { get; set; }
        public int UserProfileUserId { get; set; }
        public int ResearchgroupResearchgroupID { get; set; }
        public int UserRoleinResearchgroupID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
    
        public virtual Researchgroup Researchgroup { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProjectAssignment> UserProjectAssignments { get; set; }
    }
}
