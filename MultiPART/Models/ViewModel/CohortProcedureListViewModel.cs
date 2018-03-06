namespace MultiPART.Models.ViewModel
{
    public class CohortProcedureListViewModel
    {
        public int CohortID { get; set; }

        public int MultiPARTProjectID { get; set; }

        public string CohortLabel { get; set; }

        public int NumberOfProcedures { get; set; }

        //public string DiseaseModel { get; set; }

        public string Cormobidity { get; set; }

        public string DiseaseModelInduction { get; set; }

        public string Treatment { get; set; }

        public string OutcomeAssessment { get; set; }

        public string Anaesthesia { get; set; }

        public string Analgesia { get; set; }

        public string MortalityReport { get; set; }
    }
}