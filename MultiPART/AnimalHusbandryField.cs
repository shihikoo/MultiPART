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
    
    public partial class AnimalHusbandryField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnimalHusbandryField()
        {
            this.AnimalHusbandries = new HashSet<AnimalHusbandry>();
            this.AnimalHusbandryOptions = new HashSet<AnimalHusbandryOption>();
        }
    
        public int AnimalHusbandryFieldID { get; set; }
        public string Status { get; set; }
        public int AnimalHusbandryFieldTypeID { get; set; }
        public string AnimalHusbandryFieldName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalHusbandry> AnimalHusbandries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnimalHusbandryOption> AnimalHusbandryOptions { get; set; }
    }
}
