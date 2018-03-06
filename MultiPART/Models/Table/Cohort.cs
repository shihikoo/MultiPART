using MultiPART.Models.LinkTable;
using MultiPART.Models.LookupTable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace MultiPART.Models.Table
{
    public class Cohort:ISoftDeletable
    {
        public Cohort()
        {
            CohortProcedureAssignments = new HashSet<CohortProcedureAssignment>();
            ResearchgroupCohortAssignments = new HashSet<ResearchgroupCohortAssignment>();
            Animals = new HashSet<Animal>();

            CreatedOn = DateTimeOffset.Now;
            CreatedBy = (int)Membership.GetUser().ProviderUserKey;
            Status = "Current";
        }

        [Key]
        public int CohortID { get; set; }

        [ForeignKey("MultiPARTProjects")]
        public int MultiPARTProjectMultiPARTProjectID { get; set; }

        //[ForeignKey("DiseaseModels")]
        //public int? DiseaseModelDiseaseModelID { get; set; }

        [ForeignKey("Strain")]
        public int? StrainStrainID { get; set; }

        [ForeignKey("AnimalSuppliers")]
        public int? AnimalSupplierAnimalSupplierID { get; set; }

        //[ForeignKey("Drugs")]
        // public int DrugDrugID { get; set; }

        public string CohortLabel { get; set; }

        //public int NumberOfAnimals { get; set; }

        public int SampleSize { get; set; }

        [ForeignKey("OptionsCategoricalAge")]
         public int? CategoricalAgeID { get; set; }
        
         public string Details { get; set; }

        [ForeignKey("OptionsSex")]
         public int? SexID { get; set; }

         public float MinAge { get; set; }

         public float MaxAge { get; set; }

         public float MinWeight { get; set; }

         public float MaxWeight { get; set; }

         //[ForeignKey("AnimalConditions")]
         //public int? AnimalConditionAnimalConditionID { get; set; }

         public string Status { get; set; }

         public int CreatedBy { get; set; }

         public DateTimeOffset CreatedOn { get; set; }

         public int LastUpdatedBy { get; set; }

         private DateTimeOffset? lastUpdatedOn;
         [Required]
         public DateTimeOffset LastUpdatedOn
         {
             get { return lastUpdatedOn ?? DateTimeOffset.Now; }
             set { lastUpdatedOn = value; }
         }

         public virtual MultiPARTProject MultiPARTProjects { get; set; }
         //public virtual DiseaseModel DiseaseModels { get; set; }
         public virtual Strain Strain { get; set; }
         public virtual AnimalSupplier AnimalSuppliers { get; set; }
         //public virtual Drug Drugs { get; set; }

         //public virtual AnimalCondition AnimalConditions { get; set; }

         public virtual Option OptionsCategoricalAge { get; set; }

         public virtual Option OptionsSex { get; set; }

         public virtual ICollection<Animal> Animals { get; set; }

         public virtual ICollection<ResearchgroupCohortAssignment> ResearchgroupCohortAssignments { get; set; }        

         public virtual ICollection<CohortProcedureAssignment> CohortProcedureAssignments { get; set; }


    }
}