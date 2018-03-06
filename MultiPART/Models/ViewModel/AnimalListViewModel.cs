using System.ComponentModel;

namespace MultiPART.Models.ViewModel
{
    public class AnimalListViewModel
    {

        public int AnimalID { get; set; }

        [DisplayName("Animal Label")]
        public string AnimalLabel { get; set; }

        //public int CohortID { get; set; }

                [DisplayName("Cohort")]
        public string Cohort { get; set; }

                [DisplayName("Treatment")]
        public string Treatment { get; set; }

        //public int ResearchgroupID { get; set; }

        //public int MultiPARTProjectID { get; set; }

        //public int DiseasseModelInductionID { get; set; }

    }
}