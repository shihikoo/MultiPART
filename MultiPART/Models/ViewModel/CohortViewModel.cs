using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class CohortViewModel
    {

        public int CohortID { get; set; }

        public int MultiPARTProjectID { get; set; }

        [DisplayName("Research Group")]
        public int ResearchgroupID { get; set; }

        [DisplayName("Disease Model")]
        public int? DiseaseModelID { get; set; }

        [Required]
        [DisplayName("Strain")]
        public int? StrainID { get; set; }
        
        [DisplayName("Animal Supplier")]
        public int? AnimalSupplierID { get; set; }

        [Required]
        [DisplayName("Cohort Label")]
        public string CohortLabel { get; set; }

        [Required]
        [DisplayName("Sample Size")]
        public int? SampleSize { get; set; }
        
        [DisplayName("Notes")]
        public string Details { get; set; }

        [DisplayName("Sex")]
        public int? SexID { get; set; }

        [Required]
        [DisplayName("Minimum Age (weeks)")]
        public float? MinAge { get; set; }

        [Required]
        [DisplayName("Maximum Age (weeks)")]
        public float? MaxAge { get; set; }

        [Required]
        [DisplayName("Minimum Weight (g)")]
        public float? MinWeight { get; set; }

        [Required]
        [DisplayName("Maximum Weight (g)")]
        public float? MaxWeight { get; set; }
    }
}