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
    
    public partial class Animal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animal()
        {
            this.AnimalAdministrations = new HashSet<AnimalAdministration>();
            this.AnimalProcedures = new HashSet<AnimalProcedure>();
        }
    
        public int AnimalID { get; set; }
        public string AnimalLabel { get; set; }
        public int CohortID { get; set; }
        public int ResearchgroupID { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
        public Nullable<int> SexID { get; set; }
        public Nullable<float> Weight { get; set; }
        public Nullable<int> Age { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalAdministration> AnimalAdministrations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalProcedure> AnimalProcedures { get; set; }
        public virtual Cohort Cohort { get; set; }
        public virtual Researchgroup Researchgroup { get; set; }
    }
}