using MultiPART.Models.LinkTable;
using System.Collections.Generic;
using System.ComponentModel;

namespace MultiPART.Models.ViewModel
{
    public class ResearchgroupCohortAssignmentViewModel
    {
        public int MultiPARTProjectID { get; set; }

        public int ResearchgroupCohortAssignmentID { get; set; }

        public int CohortID { get; set; }
                        [DisplayName("Cohort Label")]
        public string CohortLabel { get; set; }

                [DisplayName("Research Group")]

        public int ResearchgroupID { get; set; }

                public string ResearchgroupName { get; set; }

        public virtual IEnumerable<ResearchgroupCohortAssignment> ResearchgroupCohortAssignments { get; set; }

        [DisplayName("Number of Animals")]
        public int NumberOfAnimals { get; set; }


        //public virtual IEnumerable<int> ResearchgroupIDs { get; set; }
    }
}