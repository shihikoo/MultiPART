using System.ComponentModel;
namespace MultiPART.Models.ViewModel
{
    public class ResearchgroupCohortAssignmentListViewModel
    {

        public int MultiPARTProjectID { get; set; }

        [DisplayName("Research Group")]
        public string ResearchgroupName { get; set; }

     
        public int ResearchgroupCohortAssignmentID { get; set; }
            [DisplayName("Number of Animals")]
        public int NumberOfAnimals { get; set; }

    }
}