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
    
    public partial class Cohort
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cohort()
        {
            this.Animals = new HashSet<Animal>();
            this.CohortProcedureAssignments = new HashSet<CohortProcedureAssignment>();
            this.ResearchgroupCohortAssignments = new HashSet<ResearchgroupCohortAssignment>();
        }
    
        public int CohortID { get; set; }
        public int MultiPARTProjectMultiPARTProjectID { get; set; }
        public Nullable<int> StrainStrainID { get; set; }
        public Nullable<int> AnimalSupplierAnimalSupplierID { get; set; }
        public string CohortLabel { get; set; }
        public Nullable<int> SampleSize { get; set; }
        public Nullable<int> CategoricalAgeID { get; set; }
        public string Details { get; set; }
        public Nullable<int> SexID { get; set; }
        public Nullable<float> MinAge { get; set; }
        public Nullable<float> MaxAge { get; set; }
        public Nullable<float> MinWeight { get; set; }
        public Nullable<float> MaxWeight { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTimeOffset CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }
        public System.DateTimeOffset LastUpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Animal> Animals { get; set; }
        public virtual AnimalSupplier AnimalSupplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CohortProcedureAssignment> CohortProcedureAssignments { get; set; }
        public virtual MultiPARTProject MultiPARTProject { get; set; }
        public virtual Strain Strain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearchgroupCohortAssignment> ResearchgroupCohortAssignments { get; set; }
    }
}
