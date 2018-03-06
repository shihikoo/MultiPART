using System.ComponentModel;

namespace MultiPART.Models.ViewModel
{
    public class CohortListViewModel
    {
        public int CohortID { get; set; }

        public int MultiPARTProjectID { get; set; }

        public string CohortLabel { get; set; }

        public string Sex { get; set; }

        public string Strain { get; set; }

        public float MinWeight  { get; set; }

        public float MaxWeight { get; set; }

        [DisplayName("Research Group")]
        public string Researchgroup { get; set; }

        public string Details { get; set; }

        public int SampleSize { get; set; }

        public int NumberOfAnimals { get; set; }

        public int NumberOfResearchgroups { get; set; }

        public int NumberOfProcedures { get; set; }

        //public string DiseaseModel { get; set; }

        public string Cormobidity { get; set; }

        [DisplayName("Disease Model Induction")]
        public string DiseaseModelInduction { get; set; }

        public string Treatment { get; set; }

        [DisplayName("Outcome Assessment")]
        public string OutcomeAssessment { get; set; }

        [DisplayName("Animal Condition")]
        public string AnimalCondition { get; set; }
    }
}