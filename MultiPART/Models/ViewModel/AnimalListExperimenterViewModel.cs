using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;

namespace MultiPART.Models.ViewModel
{
    public class AnimalListExperimenterViewModel
    {

        public int AnimalID { get; set; }

        [DisplayName("Animal Label")]
        public string AnimalLabel { get; set; }

        //public int CohortID { get; set; }

        [DisplayName("Cohort")]
        public string Cohort { get; set; }

        public int ProjectID { get; set; }
        
        public int ResearchGroupID { get; set; }

        public int DiseaseModelInductionId { get; set; }

        public IList<ProcedureViewModel> Procedures { get; set; }



    }
}