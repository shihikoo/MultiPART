using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiPART.Models.ViewModel
{
    public class AnimalViewModel
    {
        public int AnimalID { get; set; }

        [Required]
        [DisplayName("Animal Label")]
        public string AnimalLabel { get; set; }

        //[Range(0, 1000000)]
        //                [DisplayName("Weight (in gram)")]
        //public float Weight { get; set; }

        //[Range(0,100000)]
        //[DisplayName("Age (in days) ")]
        //public int? Age { get; set; }

        //public int? SexID { get; set; }

        public int CohortID { get; set; }
         [DisplayName("Cohort Label")]
        public string CohortLabel { get; set; }

        public int ResearchgroupID { get; set; }

        public int ProjectID { get; set; }

        public int DiseasseModelInductionID { get; set; }
    }
}