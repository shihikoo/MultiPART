using System.ComponentModel;

namespace MultiPART.Models.ViewModel
{
    public class UserProjectAssignmentViewModel
    {
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        public int UserProjectAssignmentID { get; set; }

                [DisplayName("Reserach Group Members")]
        public int UserInResearchgroupID { get; set; }

        [DisplayName("Role")]
        public int UserRoleinProjectID { get; set; }

        public int ResearchgroupInMultiPARTProjectID { get; set; }

        public int MultiPARTProjectID { get; set; }

        public int ResearchgroupID { get; set; }

    }
}