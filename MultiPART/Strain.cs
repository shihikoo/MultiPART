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
    
    public partial class Strain
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Strain()
        {
            this.AnimalHusbandries = new HashSet<AnimalHusbandry>();
            this.Cohorts = new HashSet<Cohort>();
        }
    
        public int StrainID { get; set; }
        public string StrainName { get; set; }
        public int SpeciesID { get; set; }
        public string Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cohort> Cohorts { get; set; }
        public virtual Species Species { get; set; }
    }
}
